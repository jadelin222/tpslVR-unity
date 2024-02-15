using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using System.Collections.Generic;

public class BlackScreenController : MonoBehaviour
{
    public AudioSource audioSource;
    public Image blackScreen;
    public TextMeshProUGUI hintText; 
    private bool isAudioFinished = false;
    //public charactermovementhelper movementHelper; // Reference to your movement helper script

   
    void Start()
    {
        audioSource.Play();
        hintText.enabled = false; // hide the hint
    }

    void Update()
    {
        if (!audioSource.isPlaying && !isAudioFinished)
        {
            hintText.enabled = true; //show the hint when audio finishes

            if (Input.GetKeyDown(KeyCode.A))
            {
                isAudioFinished = true;
                StartCoroutine(FadeOutBlackScreen());
                hintText.enabled = false; // hide the hint again
                //movementHelper.allowMovement = true;
            }
        }

        //if (!audioSource.isPlaying && !isAudioFinished)
        //{
        //    hintText.enabled = true; // Show the hint when audio finishes

        //    // Check for the "A" button press on the right-hand controller
        //    List<InputDevice> devices = new List<InputDevice>();
        //    InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        //    InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        //    foreach (var device in devices)
        //    {
        //        bool isAPressed;
        //        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out isAPressed) && isAPressed)
        //        {
        //            // when press a on controller
        //            isAudioFinished = true;
        //            StartCoroutine(FadeOutBlackScreen());
        //            hintText.enabled = false; // hide the hint again
        //            movementHelper.allowMovement = true; // enable movement
        //            break; // break the loop once the button press is detected
        //        }
        //    }
        //}


    }

    IEnumerator FadeOutBlackScreen()
    {
        float duration = 2.0f; // Fade duration in seconds
        float elapsedTime = 0;
        Color initialColor = blackScreen.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            blackScreen.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        blackScreen.gameObject.SetActive(false); // disable the black screen gameObject
    }
}