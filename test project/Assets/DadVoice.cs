
using UnityEngine;

public class DadVoice : MonoBehaviour
{
    private float playEverySeconds = 60;
    private float timePassed = 0;
    private AudioSource myAudioSource;
    public AudioSource hitSound; 
    private Animator animator;
    public string animationBoolParameter = "IsColliding"; 


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
