using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    public ToonShadingEffect toonEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (toonEffect != null)
            {
                toonEffect.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (toonEffect != null)
            {
                toonEffect.enabled = false;
            }
        }
    }
}