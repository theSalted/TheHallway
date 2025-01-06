using UnityEngine;

[ExecuteInEditMode]
public class ToonShadingEffect : MonoBehaviour
{
    public Shader toonShader;
    private Material toonMaterial;
    [Range(0.1f, 1.0f)]
    public float edgeThreshold = 0.2f;
    [Range(1, 256)]
    public int colorLevels = 4;
    public Color fillTintColor = Color.white; // Tint color for fill areas
    public Color edgeTintColor = Color.black; // Tint color for edges

    void Start()
    {
        if (toonShader == null)
        {
            toonShader = Shader.Find("Custom/ToonShadingImageEffect");
        }
        CreateMaterial();
    }

    void OnEnable()
    {
        CreateMaterial();
    }

    void OnDisable()
    {
        DestroyMaterial();
    }

    void OnDestroy()
    {
        DestroyMaterial();
    }

    void CreateMaterial()
    {
        if (toonMaterial == null)
        {
            toonMaterial = new Material(toonShader);
            toonMaterial.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    void DestroyMaterial()
    {
        if (toonMaterial)
        {
            DestroyImmediate(toonMaterial);
            toonMaterial = null;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (toonMaterial != null)
        {
            toonMaterial.SetFloat("_EdgeThreshold", edgeThreshold);
            toonMaterial.SetFloat("_ColorLevels", colorLevels);
            toonMaterial.SetColor("_FillTintColor", fillTintColor);
            toonMaterial.SetColor("_EdgeTintColor", edgeTintColor);
            Graphics.Blit(src, dest, toonMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}