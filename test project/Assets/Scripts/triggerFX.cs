using UnityEngine;

public class triggerFX : MonoBehaviour
{
    public AudioClip[] stage1Greetings;
    public AudioClip[] stage2Greetings;
    public AudioClip[] stage3Greetings;
    public AudioClip[] bagInteractions;
    public AudioClip gameOverVoiceLine;
    public GameObject[] angerSprites;

    private AudioSource audioSource;
    private Animator animator;
    private Clock clock; //reference to the Clock script
    private Transform playerTransform;
    private int currentAngerLevel = 0;

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
        ManageAngerBasedOnTimeAndTasks();
    }

    void ManageAngerBasedOnTimeAndTasks()
    {
        // Initial anger level based on time stages
        //currentAngerLevel = (clock.minutes / 15) % 4;
        currentAngerLevel = (clock.minutes / 15) % 4;
        //Debug.Log(currentAngerLevel);
        //clock.GetCurrentStage();
        //currentAngerLevel = currentStage;


        if (Consumer.teethBrushed) currentAngerLevel--;
        if (Consumer.foodEaten) currentAngerLevel--;
        if (BagInteraction.bagPacked) currentAngerLevel--; 
        //if (BagInteraction.task2Completed) currentAngerLevel--; // Example

        // Clamp the anger level to ensure it doesn't go below 0
        currentAngerLevel = Mathf.Clamp(currentAngerLevel, 0, angerSprites.Length);

        // Update sprite visibility based on the current anger level
        for (int i = 0; i < angerSprites.Length; i++)
        {
            angerSprites[i].SetActive(i < currentAngerLevel);
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
        GetComponent<NPCRandomWalk>().enabled = false;
        int currentStage = clock.GetCurrentStage(); // get current stage from the Clock
        AudioClip[] selectedGreetings = stage1Greetings; // default to stage 1 greetings

        // update the animator with the current stage
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
    public void PlayEndGameVoiceLine()
    {
        audioSource.PlayOneShot(gameOverVoiceLine);
        animator.SetBool("isSpeaking", true); // start speaking animation
        Invoke("StopSpeaking", gameOverVoiceLine.length);
        ////also invoke game over ui when audio done playing
        //Invoke("ShowEndScreen", gameOverVoiceLine.length);
    }

    void StopSpeaking()
    {
        GetComponent<NPCRandomWalk>().enabled = true;
        animator.SetBool("isSpeaking", false); // return to idle state

    }
    void FacePlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; //keep the rotation only on the Y axis

        //smooth rotation towards the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust the 5f as needed for rotation speed
    }
}
