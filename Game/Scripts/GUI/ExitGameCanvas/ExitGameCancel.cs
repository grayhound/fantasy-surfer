using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameCancel : MonoBehaviour {
    public ExitGame exit_game;

	void Start ()
    {		
	}
	
	void Update ()
    {		
	}

    public void OnClickAction()
    {
        exit_game.HideExitGameCanvas();
    }
}
