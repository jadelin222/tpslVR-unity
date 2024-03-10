using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagInteraction : MonoBehaviour
{
    public triggerFX npcScript; // Reference to the NPC's script
    public static bool bagPacked = false;

    public string[] requiredItems; // Array of required item identifiers
    public AudioClip taskCompletedSound; // Sound to play when task is completed
    private AudioSource audioSource;
    private HashSet<string> collectedItems = new HashSet<string>(); // Tracks collected items
    //private int itemsCollected = 0; // Counter for collected items
    //public int requiredItemsCount = 3;
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float chance = Random.Range(0f, 1f);

            if (chance < 0.5f)
            {
                npcScript.PlayBagInteraction(); //play a random interaction voice line 
            }
        }
        else
        {
            ItemIdentifier itemIdentifier = other.GetComponent<ItemIdentifier>();
            if (itemIdentifier != null)
            {
                foreach (string itemId in requiredItems)
                {
                    if (itemIdentifier.itemType == itemId)
                    {
                        if (!collectedItems.Contains(itemId)) // Check if the item hasn't been collected yet
                        {
                            collectedItems.Add(itemId); // Add the item to the collected set
                            StartCoroutine(ShrinkAndDisappear(other.gameObject)); // Start the shrink animation for the item

                            if (collectedItems.Count == requiredItems.Length) // Check if all items have been collected
                            {
                                TaskCompleted(); // Handle task completion logic
                            }
                            break; // Exit the loop once the item is found and processed
                        }
                    }
                }
            }
        }
    }

    IEnumerator ShrinkAndDisappear(GameObject item)
    {
        // Animation to shrink the item
        float duration = 1.0f; // Duration of the shrink animation
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(item); // Destroy the item after the animation
    }

    void TaskCompleted()
    {

        Debug.Log("bagtaskcompleted");
        bagPacked = true;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(taskCompletedSound); // Play the task completed sound
        }
        else
        {
            Debug.LogError("AudioSource component not found on the GameObject.");
        }
  
    }
}