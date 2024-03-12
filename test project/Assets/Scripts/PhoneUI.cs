using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PhoneUI : MonoBehaviour
{
    public GameObject phone;  
    public GameObject phoneCanvas;
    public AudioSource ringingSound;
    private Animator animator;

    private bool hasPhoneRung = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        phoneCanvas.SetActive(false); //initially hide the canvas
        ringingSound.Play(); //start the ringing sound
        animator.SetBool("IsVibrating", true);
    }

    void Update()
    {
        XRGrabInteractable phoneInteractable = phone.GetComponent<XRGrabInteractable>();

        if (phoneInteractable.isSelected)
        {
            phoneCanvas.SetActive(true);

            if (!hasPhoneRung) // Check if this is the first pickup
            {
                ringingSound.Stop();
                animator.SetBool("IsVibrating", false);
                hasPhoneRung = true;
            }
        }
        else
        {
            phoneCanvas.SetActive(false);
        }
    }

    
}

