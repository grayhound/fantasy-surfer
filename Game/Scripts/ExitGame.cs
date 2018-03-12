using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {
    public GameObject exitGameCanvasObject;
    public CanvasToggler canvasToggler;   

    private bool is_exit_game = false;
    private bool is_enabled = true;    

	void Start()
    {                
	}
	
	void Update()
    {
        if (is_enabled) {            
            if (CheckExitGameShow()) {
                return;
            }
            if (CheckExitGameHide()) {
                return;
            }
        }
	}

    public void Enable()
    {        
        is_enabled = true;
    }

    public void Disable()
    {
        is_enabled = false;
        if (is_exit_game) {
            HideExitGameCanvas();
        }
    }

    public bool IsEnabled()
    {
        return is_enabled;
    }

    public void ShowExitGameCanvas()
    {        
        is_exit_game = true;
        canvasToggler.ShowMainCanvas(exitGameCanvasObject);
    }

    public void HideExitGameCanvas()
    {
        is_exit_game = false;
        canvasToggler.HideMainCanvas(exitGameCanvasObject);
    }

    private bool CheckExitGameShow()
    {        
        if (Input.GetKeyDown(KeyCode.Escape) && !is_exit_game) {
            ShowExitGameCanvas();
            return true;
        }
        return false;
    }

    private bool CheckExitGameHide()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && is_exit_game) {
            HideExitGameCanvas();
            return true;
        }
        return false;
    }
}
