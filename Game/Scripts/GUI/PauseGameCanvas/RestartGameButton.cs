using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameButton : MonoBehaviour {
    public RestartGame restartGame;

    public void OnClickAction()
    {
        restartGame.ShowMenuCanvas();
    }
}
