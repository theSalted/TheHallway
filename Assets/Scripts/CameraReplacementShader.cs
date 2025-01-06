using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraReplacementShader : MonoBehaviour
{
    public Shader replacementShader;
    public string replacementTag = "";

    void OnEnable()
    {
        if (replacementShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(replacementShader, replacementTag);
        }
    }

    void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}