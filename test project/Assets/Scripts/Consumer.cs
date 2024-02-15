using UnityEngine;

public class Consumer : MonoBehaviour
{
    Collider _collider;
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

    }

}
