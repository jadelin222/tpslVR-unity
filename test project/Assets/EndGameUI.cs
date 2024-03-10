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

    //public void ShowGameOverUI(bool didWin)
    //{
    //    endGameCanvas.SetActive(true);
    //    TextMeshProUGUI messageText = endGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
    //    messageText.text = didWin ? "You Win!" : "Game Over";
    //}
    public void ShowGameOverUI()
    {
        endGameCanvas.SetActive(true);
        TextMeshProUGUI messageText = endGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "Game Over";
    }
    public void ShowGameWinUI()
    {
        endGameCanvas.SetActive(true);
        TextMeshProUGUI messageText = endGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "Game win";
    }
}
