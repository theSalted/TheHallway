using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    // Assign the object you want to enable in the Inspector
    public GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Enable the specified object
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No object assigned to enable.");
            }
        }
    }
}
