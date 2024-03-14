
using UnityEngine;

public class DadVoice : MonoBehaviour
{
    //private float playEverySeconds = 60;
    //private float timePassed = 0;
    private AudioSource myAudioSource;
    public AudioSource hitSound; 
    private Animator animator;
    public string animationBoolParameter = "IsColliding";
    public AnimationClip targetClipForEvent;


    // Start is called before the first frame update
    void Start()
    {
     
        myAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        //tried to find the clip index too but they're all the same name&cannot be renamed
        //for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        //{
        //    Debug.Log("Clip Index: " + i + " Name: " + animator.runtimeAnimatorController.animationClips[i].name);
        //}
        AddEventToClip(targetClipForEvent, 0.5f, "PlayCoughSound", 0);
    }

    //use this function to manually select the clip to insert an event to the clip,
    //as the imported animation is Read-only, events cant be inserted in unity
    void AddEventToClip(AnimationClip clip, float time, string functionName, float floatParameter)
    {
        if (clip != null)
        {
            AnimationEvent animationEvent = new AnimationEvent
            {
                functionName = functionName,
                time = time,
                floatParameter = floatParameter
            };
            clip.AddEvent(animationEvent);
        }
      
    }

    //Xinde's original implementation, playing coughing sound every 60s, not ideal as it doesnt align with animation clip.
    // Update is called once per frame
    //void Update()
    //{
    //    timePassed += Time.deltaTime;
    //    if (timePassed >= playEverySeconds)
    //    {
    //        timePassed = 0;
    //        myAudioSource.Play();
    //    }
    //}

    public void PlayCoughSound()
    {
        myAudioSource.Play();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ExamPaper")
        {
            hitSound.Play();
            animator.SetBool(animationBoolParameter, true);
            Debug.Log("Exam paper hit");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "ExamPaper")
        {
            animator.SetBool(animationBoolParameter, false);
        }
    }

}
