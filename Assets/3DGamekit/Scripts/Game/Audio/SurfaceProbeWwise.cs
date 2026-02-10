using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public sealed class SurfaceProbeWwise : MonoBehaviour
{
    [Serializable]
    public class SurfaceWwiseMapping
    {
        public string surfaceId;          // your internal key, e.g. "Mud"
        public string switchState;        // Wwise Switch State name, e.g. "Mud"
        public string rtpcName;           // optional RTPC name, e.g. "rtpc_foot_mud"
        public float rtpcScale = 1f;      // 1 for 0..1 RTPCs, 100 for 0..100 RTPCs
    }

    [Serializable] public class TagOverride { public string tag; public string surfaceId; }
    [Serializable] public class LayerOverride { public int layer; public string surfaceId; }
    [Serializable] public class MaterialOverride { public Material material; public string surfaceId; }

    public enum UvSource { UV0, UV1 }

    [Header("Raycast")]
    [SerializeField] private Transform probeOrigin;
    [SerializeField] private float rayStartUpOffset = 0.15f;
    [SerializeField] private float rayLength = 1.5f;
    [SerializeField] private LayerMask groundMask = ~0;

    [Header("Overrides (override terrain/mask)")]
    [SerializeField] private List<TagOverride> tagOverrides = new();
    [SerializeField] private List<LayerOverride> layerOverrides = new();

    [Header("Material fallback (mesh terrains etc.)")]
    [SerializeField] private List<MaterialOverride> materialOverrides = new();

    [Header("Mesh mask sampling (optional)")]
    [SerializeField] private bool sampleMeshMask = true;
    [SerializeField] private UvSource uvSource = UvSource.UV0;
    [SerializeField] private bool normalizeMaskWeights = true;

    [Header("Wwise")]
    [SerializeField] private string switchGroup = "Surface";
    [SerializeField] private string defaultSurfaceId = "Default";
    [SerializeField] private List<SurfaceWwiseMapping> surfaces = new();

    [Header("Debug")]
    [SerializeField] private bool debugLogs = true;
    [SerializeField] private bool debugLogEveryProbe = false;      // if false: logs only on changes
    [SerializeField] private bool debugDrawRay = true;
    [SerializeField] private float debugRayDrawDuration = 0.25f;
    [SerializeField] private bool debugLogMaterials = true;
    [SerializeField] private bool debugLogMaskWeights = true;

    // runtime caches
    private readonly Dictionary<string, SurfaceWwiseMapping> _surfaceMap = new(StringComparer.Ordinal);
    private readonly Dictionary<string, string> _tagMap = new(StringComparer.Ordinal);
    private readonly Dictionary<int, string> _layerMap = new();
    private readonly Dictionary<Material, string> _materialMap = new();

    // debug “change detection”
    private string _lastSurfaceId;
    private int _lastHitObjectId;

    private void Awake() => RebuildMaps();
    private void OnValidate() => RebuildMaps();

    private void RebuildMaps()
    {
        _surfaceMap.Clear();
        foreach (var s in surfaces)
        {
            if (s == null) continue;
            if (string.IsNullOrWhiteSpace(s.surfaceId)) continue;
            _surfaceMap[s.surfaceId] = s;
        }

        _tagMap.Clear();
        foreach (var t in tagOverrides)
        {
            if (t == null) continue;
            if (string.IsNullOrWhiteSpace(t.tag) || string.IsNullOrWhiteSpace(t.surfaceId)) continue;
            _tagMap[t.tag] = t.surfaceId;
        }

        _layerMap.Clear();
        foreach (var l in layerOverrides)
        {
            if (l == null) continue;
            if (string.IsNullOrWhiteSpace(l.surfaceId)) continue;
            _layerMap[l.layer] = l.surfaceId;
        }

        _materialMap.Clear();
        foreach (var m in materialOverrides)
        {
            if (m == null) continue;
            if (m.material == null || string.IsNullOrWhiteSpace(m.surfaceId)) continue;
            _materialMap[m.material] = m.surfaceId;
        }
    }

    public void ApplySurfaceTo(GameObject wwiseTarget)
    {
        if (wwiseTarget == null) return;

        var originT = probeOrigin != null ? probeOrigin : wwiseTarget.transform;
        var origin = originT.position + Vector3.up * rayStartUpOffset;

        bool hitFound = Physics.Raycast(origin, Vector3.down, out var hit, rayLength, groundMask, QueryTriggerInteraction.Ignore);

        if (debugDrawRay)
        {
            Debug.DrawRay(origin, Vector3.down * rayLength, hitFound ? Color.green : Color.red, debugRayDrawDuration);
            if (hitFound) Debug.DrawLine(origin, hit.point, Color.cyan, debugRayDrawDuration);
        }

        if (!hitFound)
        {
            MaybeLog(null, defaultSurfaceId, "NO HIT (raycast missed)");
            ApplySingleSurface(wwiseTarget, defaultSurfaceId);
            return;
        }

        // Build some shared “raycast sample” info
        var hitGO = hit.collider != null ? hit.collider.gameObject : null;
        int hitId = hitGO != null ? hitGO.GetInstanceID() : 0;

        // 1) Tag override
        string tag = hit.collider != null ? hit.collider.tag : "";
        if (!string.IsNullOrEmpty(tag) && _tagMap.TryGetValue(tag, out var tagSurface))
        {
            MaybeLog(hit, tagSurface, $"TAG OVERRIDE: tag='{tag}'");
            ApplySingleSurface(wwiseTarget, tagSurface);
            return;
        }

        // 2) Layer override
        int layer = hitGO != null ? hitGO.layer : 0;
        if (_layerMap.TryGetValue(layer, out var layerSurface))
        {
            MaybeLog(hit, layerSurface, $"LAYER OVERRIDE: layer='{LayerToString(layer)}'");
            ApplySingleSurface(wwiseTarget, layerSurface);
            return;
        }

        // 3) Vertex color mask sampling (matches your shader)
        if (sampleMeshMask)
        {
            var vMask = hit.collider.GetComponentInParent<MeshVertexColorSurfaceMask>();
            if (vMask != null)
            {
                var weights = vMask.SampleWeights(hit, normalizeMaskWeights);
                string dominant = vMask.GetDominantSurface(weights, out float domW);
                if (string.IsNullOrWhiteSpace(dominant)) dominant = defaultSurfaceId;

                // reuse your existing debug method if you want
                // MaybeLogMask(hit, dominant, $"VERTEX MASK SAMPLE rgba from mesh colors", weights, dominant, domW);

                ApplySwitch(wwiseTarget, dominant);
                ApplyRtpcs(wwiseTarget, weights);
                return;
            }
        }

        // 4) Material fallback
        var renderer = hit.collider.GetComponentInParent<Renderer>();
        if (renderer != null)
        {
            var mats = renderer.sharedMaterials;

            if (debugLogs && debugLogMaterials)
            {
                var sb = new StringBuilder();
                sb.Append($"[SurfaceProbe] MATERIALS on '{renderer.gameObject.name}': ");
                if (mats == null || mats.Length == 0) sb.Append("(none)");
                else
                {
                    for (int i = 0; i < mats.Length; i++)
                    {
                        if (i > 0) sb.Append(", ");
                        sb.Append(mats[i] != null ? mats[i].name : "NULL");
                    }
                }
                Debug.Log(sb.ToString(), this);
            }

            if (mats != null)
            {
                foreach (var mat in mats)
                {
                    if (mat != null && _materialMap.TryGetValue(mat, out var matSurface))
                    {
                        MaybeLog(hit, matSurface, $"MATERIAL FALLBACK: mat='{mat.name}'");
                        ApplySingleSurface(wwiseTarget, matSurface);
                        return;
                    }
                }
            }
        }

        // 5) Default
        MaybeLog(hit, defaultSurfaceId, "FALLBACK: default surface");
        ApplySingleSurface(wwiseTarget, defaultSurfaceId);
    }

    private void ApplySingleSurface(GameObject target, string surfaceId)
    {
        ApplySwitch(target, surfaceId);

        // Set RTPCs: chosen surface = 1, others = 0
        foreach (var s in surfaces)
        {
            if (s == null) continue;
            if (string.IsNullOrWhiteSpace(s.rtpcName)) continue;

            float v = string.Equals(s.surfaceId, surfaceId, StringComparison.Ordinal) ? 1f : 0f;
            AkUnitySoundEngine.SetRTPCValue(s.rtpcName, v * s.rtpcScale, target);
        }
    }

    private void ApplySwitch(GameObject target, string surfaceId)
    {
        if (string.IsNullOrWhiteSpace(switchGroup)) return;

        if (!_surfaceMap.TryGetValue(surfaceId, out var mapping) || string.IsNullOrWhiteSpace(mapping.switchState))
        {
            if (!_surfaceMap.TryGetValue(defaultSurfaceId, out mapping) || string.IsNullOrWhiteSpace(mapping.switchState))
                return;
        }

        AkUnitySoundEngine.SetSwitch(switchGroup, mapping.switchState, target);
    }

    private void ApplyRtpcs(GameObject target, Dictionary<string, float> weightsBySurface)
    {
        foreach (var s in surfaces)
        {
            if (s == null) continue;
            if (string.IsNullOrWhiteSpace(s.rtpcName)) continue;

            weightsBySurface.TryGetValue(s.surfaceId, out float w);
            AkUnitySoundEngine.SetRTPCValue(s.rtpcName, w * s.rtpcScale, target);
        }
    }

    private void MaybeLog(RaycastHit? hitOpt, string resolvedSurface, string decision)
    {
        if (!debugLogs) return;

        int hitId = 0;
        string hitName = "(none)";
        string hitTag = "(none)";
        string hitLayer = "(none)";

        if (hitOpt.HasValue)
        {
            var hit = hitOpt.Value;
            var go = hit.collider != null ? hit.collider.gameObject : null;
            if (go != null)
            {
                hitId = go.GetInstanceID();
                hitName = go.name;
                hitTag = go.tag;
                hitLayer = LayerToString(go.layer);
            }

            if (!debugLogEveryProbe)
            {
                if (resolvedSurface == _lastSurfaceId && hitId == _lastHitObjectId)
                    return;
            }

            _lastSurfaceId = resolvedSurface;
            _lastHitObjectId = hitId;

            var sb = new StringBuilder();
            sb.Append("[SurfaceProbe] ");
            sb.Append(decision);
            sb.Append(" | resolvedSurfaceId='").Append(resolvedSurface).Append("'");

            sb.Append("\n  HIT: ").Append(hitName)
              .Append(" | tag=").Append(hitTag)
              .Append(" | layer=").Append(hitLayer);

            sb.Append("\n  point=").Append(hit.point.ToString("F3"))
              .Append(" normal=").Append(hit.normal.ToString("F3"))
              .Append(" dist=").Append(hit.distance.ToString("F3"));

            sb.Append("\n  uv0=").Append(hit.textureCoord.ToString("F3"))
              .Append(" uv1=").Append(hit.textureCoord2.ToString("F3"))
              .Append(" tri=").Append(hit.triangleIndex);

            // mesh collider info (useful when UVs/triangles are unexpected)
            if (hit.collider is MeshCollider mc)
            {
                sb.Append("\n  MeshCollider: convex=").Append(mc.convex)
                  .Append(" mesh=").Append(mc.sharedMesh != null ? mc.sharedMesh.name : "NULL");
            }

            Debug.Log(sb.ToString(), this);
        }
        else
        {
            if (!debugLogEveryProbe && resolvedSurface == _lastSurfaceId && _lastHitObjectId == 0)
                return;

            _lastSurfaceId = resolvedSurface;
            _lastHitObjectId = 0;

            Debug.Log($"[SurfaceProbe] {decision} | resolvedSurfaceId='{resolvedSurface}'", this);
        }
    }

    private void MaybeLogMask(RaycastHit hit, string resolvedSurface, string decisionPrefix,
        Dictionary<string, float> weights, string dominant, float domW)
    {
        if (!debugLogs) return;

        int hitId = hit.collider != null ? hit.collider.gameObject.GetInstanceID() : 0;
        if (!debugLogEveryProbe && resolvedSurface == _lastSurfaceId && hitId == _lastHitObjectId)
            return;

        _lastSurfaceId = resolvedSurface;
        _lastHitObjectId = hitId;

        var sb = new StringBuilder();
        sb.Append("[SurfaceProbe] ").Append(decisionPrefix);
        sb.Append(" | resolvedSurfaceId='").Append(resolvedSurface).Append("'");

        var go = hit.collider != null ? hit.collider.gameObject : null;
        sb.Append("\n  HIT: ").Append(go != null ? go.name : "NULL")
          .Append(" | tag=").Append(go != null ? go.tag : "NULL")
          .Append(" | layer=").Append(go != null ? LayerToString(go.layer) : "NULL");

        sb.Append("\n  point=").Append(hit.point.ToString("F3"))
          .Append(" normal=").Append(hit.normal.ToString("F3"))
          .Append(" dist=").Append(hit.distance.ToString("F3"));

        sb.Append("\n  uv0=").Append(hit.textureCoord.ToString("F3"))
          .Append(" uv1=").Append(hit.textureCoord2.ToString("F3"))
          .Append(" tri=").Append(hit.triangleIndex);

        sb.Append("\n  dominant='").Append(dominant).Append("' (").Append(domW.ToString("F3")).Append(")");

        if (debugLogMaskWeights)
        {
            sb.Append("\n  weights:");
            if (weights == null || weights.Count == 0)
            {
                sb.Append(" (none)");
            }
            else
            {
                foreach (var kv in weights)
                    sb.Append("\n    ").Append(kv.Key).Append(" = ").Append(kv.Value.ToString("F3"));
            }
        }

        Debug.Log(sb.ToString(), this);
    }

    private static string LayerToString(int layer)
    {
        var name = LayerMask.LayerToName(layer);
        return string.IsNullOrEmpty(name) ? layer.ToString() : $"{name}({layer})";
    }
}
