using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool rotate;

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            transform.localRotation = UnityEngine.Quaternion.Euler(0, Time.time * 60, 0);
    }
}
