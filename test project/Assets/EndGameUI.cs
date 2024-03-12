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
        messageText.text = "Game Over. mom is angry, you're late for school, but I mean, it's not your fault, afterall, who can work under such pressure?";
    }
    public void ShowGameWinUI()
    {
        endGameCanvas.SetActive(true);
        TextMeshProUGUI messageText = endGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "Game win, run away from this toxic household and never comeb back again!";
    }
}
