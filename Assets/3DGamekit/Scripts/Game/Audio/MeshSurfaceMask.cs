using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public sealed class MeshSurfaceMask : MonoBehaviour
{
    public enum Channel { R, G, B, A }

    [Serializable]
    public class ChannelBinding
    {
        public string surfaceId;
        public Channel channel;
        public float multiplier = 1f;
    }

    [Header("Mask texture (must be Read/Write enabled)")]
    [SerializeField] private Texture2D mask;

    [Header("Channel -> Surface mapping")]
    [SerializeField] private List<ChannelBinding> bindings = new();

    [Header("Debug")]
    [SerializeField] private bool debugMaskSampling = false;

    public bool IsUsable => mask != null && mask.isReadable && bindings != null && bindings.Count > 0;

    public Dictionary<string, float> SampleWeights(Vector2 uv, bool normalize)
    {
        var result = new Dictionary<string, float>(StringComparer.Ordinal);

        if (!IsUsable)
            return result;

        Color c = mask.GetPixelBilinear(uv.x, uv.y);

        float sum = 0f;
        for (int i = 0; i < bindings.Count; i++)
        {
            var b = bindings[i];
            if (b == null || string.IsNullOrWhiteSpace(b.surfaceId)) continue;

            float v = b.channel switch
            {
                Channel.R => c.r,
                Channel.G => c.g,
                Channel.B => c.b,
                Channel.A => c.a,
                _ => 0f
            };

            v *= b.multiplier;
            v = Mathf.Clamp01(v);

            result[b.surfaceId] = v;
            sum += v;
        }

        if (normalize && sum > 0.0001f)
        {
            var keys = new List<string>(result.Keys);
            for (int i = 0; i < keys.Count; i++)
                result[keys[i]] = result[keys[i]] / sum;
        }

        if (debugMaskSampling)
        {
            int px = Mathf.Clamp(Mathf.RoundToInt(uv.x * (mask.width - 1)), 0, mask.width - 1);
            int py = Mathf.Clamp(Mathf.RoundToInt(uv.y * (mask.height - 1)), 0, mask.height - 1);

            var sb = new StringBuilder();
            sb.Append($"[MeshSurfaceMask] '{gameObject.name}' uv={uv:F3} px=({px},{py}) rgba=({c.r:F3},{c.g:F3},{c.b:F3},{c.a:F3})");
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
