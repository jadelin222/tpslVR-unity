using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public AudioClip[] greetings;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.name == "XR Origin(XR Rig)")
        {
            Debug.Log("collided!");
            PlayRandomGreeting();
        }
           
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger Entered by: " + other.name);
    //    if (other.CompareTag("Player"))
    //    {
    //        PlayRandomGreeting();
    //    }
    //}

    void PlayRandomGreeting()
    {
        int index = Random.Range(0, greetings.Length);
        audioSource.clip = greetings[index];
        audioSource.Play();
        animator.SetBool("isSpeaking", true); // Start speaking animation
        Invoke("StopSpeaking", audioSource.clip.length); // Schedule the end of the speaking animation
    }

    void StopSpeaking()
    {
        animator.SetBool("isSpeaking", false); // Return to idle state
    }
}