using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        // iterate through child objects
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);

            childTransform.LookAt(childTransform.position + mainCameraTransform.rotation * Vector3.forward,
                                  mainCameraTransform.rotation * Vector3.up);
        }
    }
}
