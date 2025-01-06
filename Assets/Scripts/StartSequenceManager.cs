using UnityEngine;
using System.Collections;
using TMPro; // Import TextMesh Pro namespace

public class GameSequenceManager : MonoBehaviour
{
    public InputAssets.FirstPersonController playerController;
    public AgentFollowWaypoints agentController;
    public GameObject blackoutPanel;
    public TextMeshProUGUI wakeUpText;    // Updated to TextMeshProUGUI
    public TextMeshProUGUI followMeText;  // Updated to TextMeshProUGUI

    public float initialDelay = 2f;
    public float wakeUpDisplayTime = 3f;
    public float betweenTextsDelay = 2f;
    public float followMeDisplayTime = 3f;
    public float agentStartDelay = 1f;
    public float playerMovementDelay = 1f;

    void Start()
    {
        // Initial setup
        blackoutPanel.SetActive(true);
        wakeUpText.gameObject.SetActive(false);
        followMeText.gameObject.SetActive(false);

        // Disable player movement and camera movement
        playerController.allowMovement = false;
        playerController.allowCameraRotation = false;

        // Ensure agent is not following
        agentController.startFollowing = false;

        // Start the sequence
        StartCoroutine(GameSequence());
    }

    IEnumerator GameSequence()
    {
        // Wait for initial delay
        yield return new WaitForSeconds(initialDelay);

        // Show "Wake Up" text
        wakeUpText.gameObject.SetActive(true);

        // Wait for player to read
        yield return new WaitForSeconds(wakeUpDisplayTime);

        // Hide blackout and "Wake Up" text
        blackoutPanel.SetActive(false);
        wakeUpText.gameObject.SetActive(false);

        // Enable camera movement
        playerController.allowCameraRotation = true;

        // Wait before next blackout
        yield return new WaitForSeconds(betweenTextsDelay);

        // Show blackout and "Follow Me" text
        blackoutPanel.SetActive(true);
        followMeText.gameObject.SetActive(true);

        // Wait for player to read
        yield return new WaitForSeconds(followMeDisplayTime);

        // Hide blackout and "Follow Me" text
        blackoutPanel.SetActive(false);
        followMeText.gameObject.SetActive(false);

        // Wait before starting agent movement
        yield return new WaitForSeconds(agentStartDelay);

        // Start agent movement
        agentController.startFollowing = true;

        // Wait before enabling player movement
        yield return new WaitForSeconds(playerMovementDelay);

        // Enable player movement
        playerController.allowMovement = true;
    }
}