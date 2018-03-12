using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject main_menu_canvas;
    public GameObject playerScoreCanvas;
    public Player player;
    public BGM bgm;

    public void ShowMainMenu()
    {
        playerScoreCanvas.SetActive(false);
        main_menu_canvas.SetActive(true);
        player.SetStartPosition();
        bgm.PlayMainMenuMusic();
    }

    public void HideMainMenu()
    {        
        main_menu_canvas.SetActive(false);
    }
}
