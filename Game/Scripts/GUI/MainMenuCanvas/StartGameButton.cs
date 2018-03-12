using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour {
    public Game game;

    public void OnClickAction()
    {
        game.StartGame();
    }
}
