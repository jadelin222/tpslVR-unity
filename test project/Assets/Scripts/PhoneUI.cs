using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PhoneUI : MonoBehaviour
{
    public GameObject phone;  
    public GameObject phoneCanvas;
    public AudioSource ringingSound;
    public XRBaseInteractor rightInteractor;

    private bool hasPhoneRung = false;

    void Start()
    {
        phoneCanvas.SetActive(false); //initially hide the canvas
        ringingSound.Play(); //start the ringing sound
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
                hasPhoneRung = true;
            }
        }
        else
        {
            phoneCanvas.SetActive(false);
        }
    }

    public void OnSpawnPhoneButtonPress(InputAction.CallbackContext context) // New function for button press
    {
        if (context.performed) // Check if the button was pressed this frame
        {
            SpawnPhoneInRightHand();
            phoneCanvas.SetActive(true); // Show the UI if the phone is spawned
            if (!hasPhoneRung) // Check if this is the first pickup
            {
                ringingSound.Stop();
                hasPhoneRung = true;
            }
        }
    }

    private void SpawnPhoneInRightHand()
    {
        if (rightInteractor)
        {
            phone.transform.SetParent(rightInteractor.attachTransform); // Parent the phone to the right hand attach transform
            phone.transform.localPosition = Vector3.zero; // Set the phone's local position to zero relative to the hand
            phone.transform.localRotation = Quaternion.identity; // Set the phone's local rotation to identity (no rotation)
        }
    }
}

