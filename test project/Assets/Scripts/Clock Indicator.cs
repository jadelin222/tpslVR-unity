using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool rotation;
    void Update()
    {
        if (rotation)
            transform.localRotation = UnityEngine.Quaternion.Euler(0, Time.time * 60, 0);
    }
}
