using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Consumer : MonoBehaviour
{
    Collider _collider;
    public AudioSource brushingSound;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

   void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();

        if(consumable != null && !consumable.isFinished)
        {
            consumable.Consume();
        }
        else if (other.CompareTag("Toothbrush")) // Check if the collider is tagged as "Toothbrush"
        {
            StartBrushing();
            if (!brushingSound.isPlaying)
            {
                brushingSound.Play(); 
            }
        }

    }

    void StartBrushing()
    {
        // Assume using the right hand controller for this example
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
            {
                uint channel = 0;
                float amplitude = 0.5f; // Vibration strength from 0 to 1
                float duration = 1f; // Vibration duration in seconds
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Toothbrush")) // When the toothbrush exits the mouth
        {
            StopBrushing();
            brushingSound.Stop();
        }
    }

    void StopBrushing()
    {
        // Stop the haptic feedback when the toothbrush is removed
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            device.StopHaptics();
        }
    }

}
