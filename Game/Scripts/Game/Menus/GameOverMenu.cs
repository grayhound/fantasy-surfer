using UnityEngine;

public class GameOverMenu : MonoBehaviour {
    public GameObject gameOverCanvas;

    public void ShowGameOverMenu()
    {
        gameOverCanvas.SetActive(true);
    }

    public void HideGameOverMenu()
    {
        gameOverCanvas.SetActive(false);
    }        
}