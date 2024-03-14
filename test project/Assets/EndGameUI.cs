using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{

    public GameObject endGameCanvas;
    void Start()
    {
        endGameCanvas.SetActive(false); //initially hide the canvas
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOverUI()
    {
        endGameCanvas.SetActive(true);
        TextMeshProUGUI messageText = endGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "You fail to catch the school bus today, mom is going to so angry, while the dad is negligent as always -- If you recognize any behaviors displayed within the game in yourself or others, it’s important to know change is possible and support is available. Seeking professional guidance can be a powerful step towards positive change. For parents, understanding and addressing one’s behavior is a profound way to provide a healthier environment for your family.";

    }
    public void ShowGameWinUI()
    {
        endGameCanvas.SetActive(true);
        TextMeshProUGUI messageText = endGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "You get away with it today, although mom still seems unsatisfied, and dad is always negligent -- If you see aspects of your behavior reflected in the game’s challenges, remember that recognizing the need for change is a courageous first step. Assistance from family support services or counseling can offer pathways to better communication and healthier relationships. For parents, embracing the opportunity to change and grow is one of the most significant gifts you can offer to your children.";

    }
}
