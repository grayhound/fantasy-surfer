using UnityEngine;

public class MainMenuButton : MonoBehaviour {
    public MainMenu mainMenu;
    public GameOverMenu gameOverMenu;
    
    public void OnClickAction()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("game_over_main_menu_click");
        mainMenu.ShowMainMenu();
        gameOverMenu.HideGameOverMenu();
    }
    
}