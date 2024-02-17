using UnityEngine;

public class triggerFX : MonoBehaviour
{
    public AudioClip[] stage1Greetings;
    public AudioClip[] stage2Greetings;
    public AudioClip[] stage3Greetings;
    public AudioClip[] bagInteractions;
    
    private AudioSource audioSource;
    private Animator animator;
    private Clock clock; //reference to the Clock script

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        clock = FindObjectOfType<Clock>(); // find the Clock object in the scene
    }
    
    void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player")) // player tagged as "Player"
        {
            PlayRandomGreeting();
        }
       
    }

    void PlayRandomGreeting()
    {
        int currentStage = clock.GetCurrentStage(); // get current stage from the Clock
        AudioClip[] selectedGreetings = stage1Greetings; // default to stage 1 greetings

        // Update the Animator with the current stage
        animator.SetInteger("Stage", currentStage); // access int parameter Stage in animator

        //select greeting based on the current stage
        if (currentStage == 1)
        {
            selectedGreetings = stage2Greetings;
        }
        else if (currentStage == 2)
        {
            selectedGreetings = stage3Greetings;
        }

        //check if there are greetings for the current stage
        if (selectedGreetings.Length == 0) return;

        //select a random greeting from the current stage
        int index = Random.Range(0, selectedGreetings.Length);
        audioSource.clip = selectedGreetings[index];
        audioSource.Play();
        animator.SetBool("isSpeaking", true); //start speaking animation
        Invoke("StopSpeaking", audioSource.clip.length); //stop speaking anim when audio finished
        
    }

    public void PlayBagInteraction()
    {
        if (bagInteractions.Length == 0) return; // check if there are any bag interaction lines

        int index = Random.Range(0, bagInteractions.Length); // randomly select a line for bag interaction
        audioSource.clip = bagInteractions[index];
        audioSource.Play();
        animator.SetBool("isSpeaking", true); // start speaking animation
        Invoke("StopSpeaking", audioSource.clip.length); // stop speaking animation when audio finished
    }

    void StopSpeaking()
    {
        animator.SetBool("isSpeaking", false); // return to idle state

    }
}
