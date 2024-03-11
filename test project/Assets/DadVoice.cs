using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadVoice : MonoBehaviour
{
    private float playEverySeconds = 60;
    private float timePassed = 0;
    private AudioSource myAudioSource;
    public AudioSource hitSound; 
    private Animator animator;
    public string animationBoolParameter = "IsColliding"; // The name of the Animator parameter


    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= playEverySeconds)
        {
            timePassed = 0;
            myAudioSource.Play();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
       hitSound.Play();
       animator.SetBool("IsColliding", true);
       Debug.Log("hit");
    }

    void OnTriggerExit(Collider collision)
    {
         animator.SetBool("IsColliding", false);
    }

}
