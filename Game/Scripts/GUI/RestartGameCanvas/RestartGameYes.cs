using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameYes : MonoBehaviour {
    public Game game;
    public PauseGame pauseGame;
    public RestartGame restartGame;


    public void OnClickAction()
    {
        game.StopGame();
        game.StartGame();        
        restartGame.HideMenuGameCanvas();
        pauseGame.HidePauseGameMenu();
    }
}
