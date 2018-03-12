using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameCancel : MonoBehaviour {
    public RestartGame restartGame;

    public void OnClickAction()
    {
        restartGame.HideMenuGameCanvas();
    }
}
