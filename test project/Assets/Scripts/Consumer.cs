using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Consumer : MonoBehaviour
{
    Collider _collider;
    public AudioSource brushingSound;

    //public bool allFoodConsumed = false; // Flag for tracking if all food has been eaten
    public static bool teethBrushed = false; // Flag for tracking teeth brushing
    public static bool foodEaten = false;

    //for tracking consumables
    private List<Consumable> consumables = new List<Consumable>();
    private int consumablesFinished = 0;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;

        //find all consumables in the scene and add them to the list
        consumables.AddRange(FindObjectsOfType<Consumable>());
    }

   void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();

        if(consumable != null && !consumable.isFinished)
        {
            consumable.Consume();

            // Check if the consumable is finished after consuming
            if (consumable.isFinished)
            {
                consumablesFinished++;
                //Debug.Log(consumablesFinished);
                // Check if all consumables have been finished
                if (consumablesFinished >= consumables.Count)
                {
                    //Debug.Log("All food has been eaten.");
                    //trigger any action after all food is consumed
                    foodEaten = true;
                }
            }
        }
        else if (other.CompareTag("Toothbrush")) // Check if the collider is tagged as "Toothbrush"
        {
            StartBrushing();
            if (!brushingSound.isPlaying)
            {
                brushingSound.Play(); 
            }
            teethBrushed = true;
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
