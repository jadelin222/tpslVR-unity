using UnityEditor.SceneManagement;
using UnityEngine;

public class triggerFX : MonoBehaviour
{
    public AudioClip[] stage1Greetings;//0
    public AudioClip[] stage2Greetings;//1
    public AudioClip stage1Announcement;//1
    public AudioClip[] stage3Greetings;//2
    public AudioClip stage2Announcement;//2
    public AudioClip[] bagInteractions;
    public AudioClip gameOverVoiceLine;
    public GameObject[] angerSprites;

    private AudioSource audioSource;
    private Animator animator;
    private Clock clock; //reference to the Clock script
    private Transform playerTransform;
    private int currentAngerLevel = 0;
    private int currentStage;
    private bool played720Line = false;
    private bool played740Line = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        clock = FindObjectOfType<Clock>(); // find the Clock object in the scene
    }
    void Update()
    {
        currentStage = clock.GetCurrentStage(); //get current stage from the Clock
        PlayStageAnnouncement(currentStage);
        if (animator.GetBool("isSpeaking"))
        {
            FacePlayer();
        }

        ManageAngerBasedOnTimeAndTasks();
    }

    void ManageAngerBasedOnTimeAndTasks()
    {
        //initial anger level based on time stages
        currentAngerLevel = (clock.minutes / 15) % 4;
        //Debug.Log(currentAngerLevel);
  

        if (Consumer.teethBrushed) currentAngerLevel--;
        if (Consumer.foodEaten) currentAngerLevel--;
        if (BagInteraction.bagPacked) currentAngerLevel--; 
        //if (ExamPaper.paperSigned) currentAngerLevel--; 

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

    //void PlayRandomGreeting()
    //{
    //    GetComponent<NPCRandomWalk>().enabled = false;
    //    int currentStage = clock.GetCurrentStage(); // get current stage from the Clock
    //    AudioClip[] selectedGreetings = stage1Greetings; // default to stage 1 greetings

    //    // update the animator with the current stage
    //    animator.SetInteger("Stage", currentStage); // access int parameter Stage in animator

    //    //select greeting based on the current stage
    //    if (currentStage == 1)
    //    {
    //        selectedGreetings = stage2Greetings;
    //    }
    //    else if (currentStage == 2)
    //    {
    //        selectedGreetings = stage3Greetings;
    //    }

    //    //check if there are greetings for the current stage
    //    if (selectedGreetings.Length == 0) return;
    //    //select a random greeting from the current stage
    //    int index = Random.Range(0, selectedGreetings.Length);
    //    audioSource.clip = selectedGreetings[index];
    //    audioSource.Play();
    //    animator.SetBool("isSpeaking", true); //start speaking animation
    //    Invoke("StopSpeaking", audioSource.clip.length); //stop speaking anim when audio finished
        
    //}
    void PlayRandomGreeting()
    {
        // Check if the audioSource is currently playing, indicating that a time announcement or another greeting is active
        if (audioSource.isPlaying) return;

        GetComponent<NPCRandomWalk>().enabled = false;
        //int currentStage = clock.GetCurrentStage(); // Get current stage from the Clock
        AudioClip[] selectedGreetings = null;
        // update the animator with the current stage
        animator.SetInteger("Stage", currentStage);
       //determine the appropriate set of greetings based on the current stage
        switch (currentStage)
        {
            case 0:
                selectedGreetings = stage1Greetings;
                break;
            case 1:
                selectedGreetings = stage2Greetings;
                break;
            case 2:
                selectedGreetings = stage3Greetings;
                break;
        }

        //play a random greeting if there are any for the current stage
        if (selectedGreetings != null && selectedGreetings.Length > 0)
        {
            int index = Random.Range(0, selectedGreetings.Length);
            audioSource.clip = selectedGreetings[index];
            audioSource.Play();
            animator.SetBool("isSpeaking", true); // Start speaking animation
            Invoke("StopSpeaking", audioSource.clip.length); // Schedule end of speaking animation
        }
    }

    private void PlayStageAnnouncement(int stage)
    {
        AudioClip clipToPlay = null;

        switch (stage)
        {
            case 1:
                if (!played720Line) //make sure it's only played once
                {
                    clipToPlay = stage1Announcement;
                    played720Line = true;
                }
                break;
            case 2:
                if (!played740Line) 
                {
                    clipToPlay = stage2Announcement;
                    played740Line = true;
                }
                break;
        }

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
            animator.SetBool("isSpeaking", true);
            //Invoke(nameof(StopSpeaking), clipToPlay.length);
            Invoke("StopSpeaking", clipToPlay.length);
        }
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
