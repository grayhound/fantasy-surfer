using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameButton : MonoBehaviour {
    public PauseGame pauseGame;

    public void OnClickAction()
    {
        pauseGame.HidePauseGameMenu();
    }
}
