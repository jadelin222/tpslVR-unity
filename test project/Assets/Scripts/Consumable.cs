using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] GameObject[] portions;
    [SerializeField] int index = 0;

    AudioSource _audioSource;
    public bool isFinished => index == portions.Length;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        SetVisuals();
    }

    void OnValidate()
    {
        SetVisuals();
    }

    [ContextMenu("Consume")]
    public void Consume()
    {
        if(!isFinished)
        {
            index++;
            SetVisuals();
            _audioSource.Play();
            // Stop the audio after 4 seconds
            Invoke("StopAudio", 4f);

        }
    }

    void SetVisuals()
    {
        for(int i = 0; i < portions.Length; i++)
        {
            portions[i].SetActive(i == index);
        }
    }
    void StopAudio()
    {
        _audioSource.Stop(); // Stops playing the audio
    }


}
