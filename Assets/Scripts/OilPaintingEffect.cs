using UnityEngine;

[ExecuteInEditMode]
public class OilPaintingEffect : MonoBehaviour
{
    public Shader oilPaintingShader;
    private Material oilPaintingMaterial;
    public RenderTexture maskTexture;

    void Start()
    {
        if (oilPaintingShader == null)
        {
            oilPaintingShader = Shader.Find("Custom/OilPaintingImageEffect");
        }
        oilPaintingMaterial = new Material(oilPaintingShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (oilPaintingMaterial != null && maskTexture != null)
        {
            oilPaintingMaterial.SetTexture("_MaskTex", maskTexture);
            Graphics.Blit(src, dest, oilPaintingMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}