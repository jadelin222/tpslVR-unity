using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamCollisionSound : MonoBehaviour
{
    //The collision sound's Audio Source
    public AudioSource hitSound;

    //"OnCollisionEnter" refers to when something enters collision with the object this script is attached to
    void OnCollisionEnter()
    {
        //The collision sound will play
        hitSound.Play();
    }
}
