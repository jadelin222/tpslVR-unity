using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using TMPro; 

public class openSceneSwitch : MonoBehaviour
{
    public AudioSource knockingAudio;
    public TextMeshProUGUI wakeUpText;
    public string nextSceneName = "NextScene";
    public GameObject darkScreen;

    void Start()
    {
        knockingAudio.Play();
        wakeUpText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!knockingAudio.isPlaying && !wakeUpText.gameObject.activeInHierarchy)
        {
            wakeUpText.gameObject.SetActive(true);
        }

        if (wakeUpText.gameObject.activeInHierarchy && AnyButtonPressed())
        {
            StartCoroutine(FadeOutAndLoadNextScene());
        }
    }

    bool AnyButtonPressed()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);
        foreach (var device in devices)
        {
            bool inputPressed;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out inputPressed) && inputPressed)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator FadeOutAndLoadNextScene()
    {
        
        wakeUpText.gameObject.SetActive(false);

        //fade out the dark screen
        CanvasGroup canvasGroup = darkScreen.AddComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        //Destroy(darkScreen); //destroy or deactivate the dark screen
        //after fade-out, load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
