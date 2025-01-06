using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject agentGameObject; // Assign the agent GameObject in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!agentGameObject.activeSelf)
                agentGameObject.SetActive(true);
                Debug.Log("Agent activated");
            AgentFollowWaypoints agentScript = agentGameObject.GetComponent<AgentFollowWaypoints>();
            if (agentScript != null)
            {
                Debug.Log("Agent started following");
                agentScript.startFollowing = true;
            }
        } else {
            Debug.Log("Player not detected");
        }
    }
}