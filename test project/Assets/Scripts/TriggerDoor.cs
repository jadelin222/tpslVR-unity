using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerDoor : MonoBehaviour
{

    public GameObject door;
    private Animator doorAnim;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        doorAnim = door.GetComponent<Animator>();
    }
    public void OpenCloseDoor()
    {
        if (isOpen==true)
        {
            StartCoroutine(Closing());

        }else if (isOpen == false)
        {
            StartCoroutine(Opening());
        }
    }
    IEnumerator Opening()
    {
        //print("you are opening the door");
        doorAnim.Play("Opening");
        isOpen = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator Closing()
    {
        //print("you are closing the door");
        doorAnim.Play("Closing");
        isOpen = false;
        yield return new WaitForSeconds(.5f);
    }
    
}
