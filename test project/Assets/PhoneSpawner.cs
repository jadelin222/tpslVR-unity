using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PhoneSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public Transform handTransform; 
    private InputDevice rightHandController;
    private bool isObjectHeld = false;

    void Start()
    {
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightHandDevices);
        if (rightHandDevices.Count > 0)
        {
            rightHandController = rightHandDevices[0];
        }
    }

    void Update()
    {
        if (rightHandController.isValid)
        {
            bool primaryButtonPressed = false;
            rightHandController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed);
            if (primaryButtonPressed && !isObjectHeld)
            {
                GrabObject();
            }

            //check for grip release
            bool gripButtonPressed = false;
            rightHandController.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonPressed);
            if (!gripButtonPressed && isObjectHeld)
            {
                ReleaseObject();
            }
        }
    }

    void GrabObject()
    {
        //move the object to the hand's position and rotation
        objectToSpawn.transform.position = handTransform.position;
        objectToSpawn.transform.rotation = handTransform.rotation;

        //attach the object to the hand to follow its movement
        objectToSpawn.transform.SetParent(handTransform, worldPositionStays: true);
        isObjectHeld = true;

        //make the object kinematic if it has a Rigidbody
        Rigidbody rb = objectToSpawn.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void ReleaseObject()
    {
        //detach the object from the hand
        objectToSpawn.transform.SetParent(null, worldPositionStays: true);
        isObjectHeld = false;

        //make the object non-kinematic to enable physics
        Rigidbody rb = objectToSpawn.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true; // Make sure gravity affects the object again
        }
    }

}
