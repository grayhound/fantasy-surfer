using UnityEngine;

public class TryAgainButton : MonoBehaviour {
    public Game game;

    public void OnClickAction()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("game_over_try_again_click");
        game.StartGame();
    } 
}