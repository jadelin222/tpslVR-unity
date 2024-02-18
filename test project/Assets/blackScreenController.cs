using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class WakeUpController : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject darkScreen;
    public TextMeshProUGUI hintText;
    public XRRayInteractor leftHandController; // Assign the left hand controller
    public XRDirectInteractor rightHandController; // Assign the right hand controller
    private bool hasWokenUp = false;

    void Start()
    {
        audioSource.Play();
        StartCoroutine(ShowHintAfterAudio());
    }

    IEnumerator ShowHintAfterAudio()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        hintText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!hasWokenUp && Input.GetButtonDown("Submit")) // "Submit" is typically the "A" button, but check your Input settings
        {
            StartCoroutine(WakeUp());
        }
    }

    IEnumerator WakeUp()
    {
        hasWokenUp = true;
        hintText.gameObject.SetActive(false);

        // Fade out the dark screen
        CanvasGroup canvasGroup = darkScreen.AddComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        Destroy(darkScreen); // Optional: destroy or deactivate the dark screen

        // Enable player movement (assuming movement is disabled by default)
        //leftHandController.enableInteractions = true;
        //rightHandController.enableInteractions = true;
    }
}
