using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectToggle : MonoBehaviour
{
    // Lists of objects to enable and disable
    public List<GameObject> objectsToDisable;
    public List<GameObject> objectsToEnable;

    // Specify the tag that the player should have
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the player tag
        if (other.CompareTag(playerTag))
        {
            // Disable each object in the list
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            // Enable each object in the list
            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}