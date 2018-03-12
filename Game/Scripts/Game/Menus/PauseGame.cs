using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public Game game;
    public GameObject pauseGameCanvas;
    public SFX sfx;
    public BGM bgm;

    private bool _isEnabled = false;
    private bool isPauseGame = false;

	void Start()
    {
	}
	
	void Update()
    {	
        if (_isEnabled) {
            if (_CheckPauseMenuShow()) {
                return;
            }
        }
	}

    public void Enable()
    {
        _isEnabled = true;        
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    public void TogglePauseGameMenu()
    {
        if (isPauseGame) {
            HidePauseGameMenu();
        } else {
            ShowPauseGameMenu();
        }
        
    }

    public void ShowPauseGameMenu()
    {
        sfx.PlaySfxPauseGame();
        bgm.PauseBGM();
        pauseGameCanvas.SetActive(true);
        isPauseGame = true;
        game.Pause();
    }

    public void HidePauseGameMenu()
    {
        sfx.PlaySfxPauseGame();
        bgm.UnpauseBGM();
        pauseGameCanvas.SetActive(false);
        isPauseGame = false;
        game.Unpause();
    }

    private bool _CheckPauseMenuShow()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPauseGame) {
            ShowPauseGameMenu();            
            return true;
        }  
        return false;
    }
}

