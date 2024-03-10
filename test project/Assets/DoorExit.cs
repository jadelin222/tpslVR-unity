using UnityEngine;

public class DoorExit : MonoBehaviour
{
    public EndGameUI endGameUI;
    public triggerFX npcScript;  

    public void ShowWinUI()
    {
        if (npcScript.allTasksCompleted==true)  // Check task completion
        {
            endGameUI.ShowGameWinUI(); // Tasks are complete
        }
        else
        {
            npcScript.AnnounceTasksNonCompletion();
        }
    }

}