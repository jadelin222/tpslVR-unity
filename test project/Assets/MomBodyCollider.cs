using UnityEngine;

public class MomBodyCollider : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        //notify the parent to play corresponding voice lines
        if (other.CompareTag("ExamPaper"))
        {
            GetComponentInParent<triggerFX>().PlayExamPaperVoiceLine(); ;
        }
        if (other.CompareTag("Weapon"))
        {
            GetComponentInParent<triggerFX>().PlayWeaponInteraction();
        }
    }
}
