using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhoneUI : MonoBehaviour
{
    public GameObject phone;  // Assign the phone object in the Inspector
    public GameObject phoneCanvas; // Assign your canvas object in the Inspector

    void Start()
    {
        phoneCanvas.SetActive(false); // Initially hide the canvas
    }

    void Update()
    {
        XRGrabInteractable phoneInteractable = phone.GetComponent<XRGrabInteractable>();

        if (phoneInteractable.isSelected)
        {
            phoneCanvas.SetActive(true);
        }
        else
        {
            phoneCanvas.SetActive(false);
        }
    }
}
