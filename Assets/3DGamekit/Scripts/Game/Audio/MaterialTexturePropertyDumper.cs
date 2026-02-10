using UnityEngine;

public class MaterialTexturePropertyDumper : MonoBehaviour
{
    [SerializeField] private Material material;

    [ContextMenu("Dump Texture Properties")]
    public void Dump()
    {
        if (material == null) { Debug.Log("No material assigned."); return; }
        var s = material.shader;

        Debug.Log($"--- Texture properties for material '{material.name}' (shader '{s.name}') ---");

        int count = s.GetPropertyCount();
        for (int i = 0; i < count; i++)
        {
            if (s.GetPropertyType(i) != UnityEngine.Rendering.ShaderPropertyType.Texture)
                continue;

            string prop = s.GetPropertyName(i);
            var tex = material.GetTexture(prop);
            Debug.Log($"{prop} = {(tex != null ? tex.name : "NULL")}");
        }
    }
}
