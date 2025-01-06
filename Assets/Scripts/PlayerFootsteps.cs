using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource; // The audio source component
    public AudioClip[] footstepSounds; // Array of footstep sounds
    public float stepInterval = 0.5f; // Time between steps

    private float stepTimer = 0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (characterController != null && characterController.isGrounded && characterController.velocity.magnitude > 0.2f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            Debug.Log("Playing footstep sound");
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.clip = footstepSounds[index];
            audioSource.Play();
        }
    }
}