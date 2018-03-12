using UnityEngine;

public class ExitGameButton : MonoBehaviour {
    public ExitGame exitgame;
    
    public void OnClickAction()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("game_over_exit_game_click");
        exitgame.ShowExitGameCanvas();
    }    
}