using UnityEngine;

public class PlayDelayedAudio : MonoBehaviour
{
    private AudioSource[] audioSources; // Array to store references to all AudioSources in children
    public float delay = 1.0f; // Delay in seconds before playing each audio clip
    public float stopTime = 10.0f; // Time in seconds after which to stop playing each audio clip

    void Start()
    {
        // Get all AudioSources in children
        audioSources = GetComponentsInChildren<AudioSource>();

        // Start the coroutine to play and stop audio
        StartCoroutine(PlayAndStopAudio());
    }

    private System.Collections.IEnumerator PlayAndStopAudio()
    {
        foreach (var audioSource in audioSources)
        {
            // Play audio after delay
            yield return new WaitForSeconds(delay);
            audioSource.Play();

            // Wait for stopTime and then stop audio
            yield return new WaitForSeconds(stopTime);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
