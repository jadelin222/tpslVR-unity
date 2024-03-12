using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhoneUI : MonoBehaviour
{
    public GameObject phone;
    public GameObject phoneCanvas;
    public AudioSource ringingSound;

    public float vibrationIntensity = 1f; // Adjust the intensity for noticeable but controlled vibration

    private bool isVibrating = true;

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
            ringingSound.Stop();
            isVibrating = false;
        }
        else
        {
            phoneCanvas.SetActive(false);
        }

        if (isVibrating)
        {
            VibratePhone();
        }
    }

    void VibratePhone()
    {
      
        float rotationFactor = vibrationIntensity * 10; // Increase the scale for rotation
        Vector3 randomRotationOffset = new Vector3(
            Random.Range(-rotationFactor, rotationFactor),
            Random.Range(-rotationFactor, rotationFactor),
            Random.Range(-rotationFactor, rotationFactor));

        //apply random rotation
        phone.transform.localEulerAngles += randomRotationOffset;

    }

}
