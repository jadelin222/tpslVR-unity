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
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        clock = FindObjectOfType<Clock>(); // find the Clock object in the scene
    }
    void Update()
    {
        if (animator.GetBool("isSpeaking"))
        {
            FacePlayer();
        }
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
        //FacePlayer();
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

        //FacePlayer();
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
    void FacePlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Keep the rotation only on the Y axis

        // Smooth rotation towards the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust the 5f as needed for rotation speed
    }
}
