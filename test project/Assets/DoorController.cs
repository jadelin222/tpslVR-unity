using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    private XRGrabInteractable grabInteractable;
    private bool isDoorOpen = false;

    void Awake()
    {
        grabInteractable = GetComponentInChildren<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleGrabbed);
    }

    private void HandleGrabbed(SelectEnterEventArgs args)
    {
        if (!isDoorOpen)
        {
            doorAnimator.SetBool("handleGrabbed", true); // Trigger the door to open
            isDoorOpen = true; // Mark the door as open
        }
    }

    void Update()
    {
        if (isDoorOpen)
        {
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("handleGrabbed") && stateInfo.normalizedTime >= 1) // Check if the animation has finished
            {
                doorAnimator.SetBool("handleGrabbed", false); // Reset the parameter to close the door
                isDoorOpen = false; // Reset the flag as the door is now closed
            }
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(HandleGrabbed);
        }
    }
}