using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
    public PauseGame pauseGame;

    public void OnClickAction()
    {
        pauseGame.TogglePauseGameMenu();
    }
}
