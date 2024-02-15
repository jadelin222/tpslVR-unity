using UnityEngine;

public class triggerFX : MonoBehaviour
{
    public AudioClip[] stage1Greetings;
    public AudioClip[] stage2Greetings;
    public AudioClip[] stage3Greetings;
    public AudioClip[] bagInteractions;
    //public AudioClip[] greetings;
    private AudioSource audioSource;
    private Animator animator;
    private Clock clock; // Reference to the Clock script

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        clock = FindObjectOfType<Clock>(); // Find the Clock object in the scene
    }
    
    void OnTriggerEnter(Collider other)
    {
        //audioSource.Play();
        //PlayRandomGreeting();
        if (other.CompareTag("Player")) // Assuming the player has a tag "Player"
        {
            PlayRandomGreeting();
        }
       
    }

    void PlayRandomGreeting()
    {
        int currentStage = clock.GetCurrentStage(); // Get the current stage from the Clock
        AudioClip[] selectedGreetings = stage1Greetings; // Default to stage 1 greetings

        // Select the appropriate greetings array based on the current stage
        if (currentStage == 1)
        {
            selectedGreetings = stage2Greetings;
        }
        else if (currentStage == 2)
        {
            selectedGreetings = stage3Greetings;
        }

        // Check if there are greetings for the current stage
        if (selectedGreetings.Length == 0) return;

        // Select a random greeting from the current stage
        int index = Random.Range(0, selectedGreetings.Length);
        audioSource.clip = selectedGreetings[index];
        audioSource.Play();
        animator.SetBool("isSpeaking", true); // Start speaking animation
        Invoke("StopSpeaking", audioSource.clip.length); // Schedule the end of the speaking animation the speaking animation
        //int index = Random.Range(0, greetings.Length);
        //audioSource.clip = greetings[index];
        //audioSource.Play();
        //animator.SetBool("isSpeaking", true); // Start speaking animation
        //Invoke("StopSpeaking", audioSource.clip.length); // Schedule the end of the speaking animation
    }

    public void PlayBagInteraction()
    {
        if (bagInteractions.Length == 0) return; // Check if there are any bag interaction lines

        int index = Random.Range(0, bagInteractions.Length); // Select a random bag interaction line
        audioSource.clip = bagInteractions[index];
        audioSource.Play();
        animator.SetBool("isSpeaking", true); // Start speaking animation
        Invoke("StopSpeaking", audioSource.clip.length); // Schedule the end of the speaking animation
    }

    void StopSpeaking()
    {
        animator.SetBool("isSpeaking", false); // Return to idle state

    }
}
