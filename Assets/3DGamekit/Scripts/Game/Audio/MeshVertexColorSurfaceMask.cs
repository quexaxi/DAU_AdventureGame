using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public sealed class MeshVertexColorSurfaceMask : MonoBehaviour
{
    public enum Channel { R, G, B, A }

    [Serializable]
    public class Binding
    {
        public string surfaceId;     // e.g. "Water"
        public Channel channel;      // which vertex color channel to read
        public bool invert;          // use 1 - channel (useful for "Mud = 1 - Water")
        public float multiplier = 1f;
    }

    [SerializeField] private List<Binding> bindings = new();

    [Header("Debug")]
    [SerializeField] private bool debugSampling = false;

    public bool IsUsable(MeshCollider mc)
    {
        if (mc == null || mc.sharedMesh == null) return false;
        var mesh = mc.sharedMesh;
        return mesh.colors != null && mesh.colors.Length == mesh.vertexCount && bindings != null && bindings.Count > 0;
    }

    public Dictionary<string, float> SampleWeights(RaycastHit hit, bool normalize)
    {
        var result = new Dictionary<string, float>(StringComparer.Ordinal);

        if (hit.collider is not MeshCollider mc || !IsUsable(mc))
            return result;

        var mesh = mc.sharedMesh;
        var colors = mesh.colors;
        var tris = mesh.triangles;

        int tri = hit.triangleIndex;
        int triBase = tri * 3;
        if (triBase + 2 >= tris.Length)
            return result;

        int i0 = tris[triBase + 0];
        int i1 = tris[triBase + 1];
        int i2 = tris[triBase + 2];

        Vector3 b = hit.barycentricCoordinate; // (w0,w1,w2)
        Color c0 = colors[i0];
        Color c1 = colors[i1];
        Color c2 = colors[i2];

        // Interpolated vertex color at hit point
        Color c = c0 * b.x + c1 * b.y + c2 * b.z;

        float sum = 0f;
        for (int k = 0; k < bindings.Count; k++)
        {
            var bind = bindings[k];
            if (bind == null || string.IsNullOrWhiteSpace(bind.surfaceId)) continue;

            float v = bind.channel switch
            {
                Channel.R => c.r,
                Channel.G => c.g,
                Channel.B => c.b,
                Channel.A => c.a,
                _ => 0f
            };

            if (bind.invert) v = 1f - v;
            v = Mathf.Clamp01(v * bind.multiplier);

            result[bind.surfaceId] = v;
            sum += v;
        }

        if (normalize && sum > 0.0001f)
        {
            var keys = new List<string>(result.Keys);
            for (int i = 0; i < keys.Count; i++)
                result[keys[i]] = result[keys[i]] / sum;
        }

        if (debugSampling)
        {
            var sb = new StringBuilder();
            sb.Append($"[MeshVertexColorSurfaceMask] '{gameObject.name}' tri={tri} bary={b:F3} vertColorRGBA=({c.r:F3},{c.g:F3},{c.b:F3},{c.a:F3})");
            foreach (var kv in result)
                sb.Append($"\n  {kv.Key}={kv.Value:F3}");
            Debug.Log(sb.ToString(), this);
        }

        return result;
    }

    public string GetDominantSurface(Dictionary<string, float> weights, out float weight)
    {
        string best = null;
        float bestW = -1f;

        foreach (var kv in weights)
        {
            if (kv.Value > bestW)
            {
                bestW = kv.Value;
                best = kv.Key;
            }
        }

        weight = bestW;
        return best;
    }
}
