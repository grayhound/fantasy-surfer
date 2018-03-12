using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Game : MonoBehaviour {
    public MainMenu main_menu;
    public ExitGame exit_game;
    public PlayerController player_controller;
    public PlayerGun playerGun;
    public Spawner spawner;
    public PlayerScore playerScore;
    public PauseGame pauseGame;
    public Player player;
    public BGM bgm;    
    public SkyboxRotate skybox;
    public AdMobBottomBanner bottomBanner;
    public AdMobGameOverInterstitial gameOverInterstitial;
    public PlayerBonusStatus playerBonusStatus;
    public PleaseRate pleaseRate;

    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;
    public GameObject gameLevelCanvas;

    private void Awake()
    {        
        Application.targetFrameRate = 60;
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);
        
        PrepareObjectPool();
        DOTween.Init().SetCapacity(200, 20);
    }

    void Start ()
    {
        main_menu.ShowMainMenu();
    }
        
    public void StartGame()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("start_game");
        Spawner.ClearSpawnedObjects();
        player.CheckBonusGames();
        main_menu.HideMainMenu();
        exit_game.Disable();
        player_controller.EnableController();
        spawner.StartSpawner();
        playerScore.StartPlayerScore();
        pauseGame.Enable();
        gameMenuCanvas.SetActive(true);
        gameLevelCanvas.SetActive(true);
        player.ResetPlayer();
        player.ShowPlayerLiveContainers();
        gameOverCanvas.SetActive(false);
        bgm.PlayRandomGameMusic();
        skybox.ReloadSkybox();
        bottomBanner.HideBanner();
        gameOverInterstitial.PrepareBanner();
        player.RemoveLifeBonus();
        player.RemoveAutoShieldBonus();
        playerGun.RemoveDoubleFireBonus();
        playerBonusStatus.EnablePlayerBonusStatus();
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        exit_game.Enable();
        playerGun.PauseGun();
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        exit_game.Disable();
        playerGun.UnpauseGun();
    }

    public void GameOver()
    {        
        StopGame();
        pauseGame.Disable();
        exit_game.Enable();
        gameOverCanvas.SetActive(true);
        gameMenuCanvas.SetActive(false);
        gameLevelCanvas.SetActive(false);
        bgm.PlayGameOverMusic();
        player_controller.DisableController();
        player.StopPlayer();
        pleaseRate.AddGamesPlayed();
        pleaseRate.CheckPleaseRatePopup();
                
        gameOverInterstitial.ShowBanner();
        bottomBanner.ShowBanner();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("game_over");
    }

    public void StopGame()
    {
        player.HidePlayerLiveContainers();
        player_controller.DisableController();
        spawner.StopSpawner();
        playerScore.SendPlayerScore();
        playerScore.StopPlayerScore();        
        playerBonusStatus.DisablePlayerBonusStatus();
        playerGun.StopGun();
    }

    public void PauseGameDeath()
    {
        player.PausePlayer();
        player_controller.PauseController();
        playerGun.PauseGun();
        spawner.PauseSpawner();
        playerScore.PausePlayerScore();
        bgm.PauseBGM();
        playerBonusStatus.PauseBonusStatus();
    }

    public void UnpauseGameDeath()
    {
        player.UnpausePlayer();
        player_controller.UnpauseController();
        playerGun.UnpauseGun();
        spawner.UnpauseSpawner();        
        playerScore.UnpausePlayerScore();
        bgm.UnpauseBGM();
        playerBonusStatus.UnpauseBonusStatus();
    }

    public Spawner GetSpawner()
    {
        return spawner;
    }

    public PlayerScore GetPlayerScore()
    {
        return playerScore;
    }

    private void PrepareObjectPool()
    {        
        ObjectPool.Clear();        
        ObjectPool.CreatePool("coin", GameObject.Find("/Resources/Coin"), 50);
        ObjectPool.CreatePool("star", GameObject.Find("/Resources/Star"), 5);
        ObjectPool.CreatePool("speedup", GameObject.Find("/Resources/Speedup"), 5);
        ObjectPool.CreatePool("bullet_bonus", GameObject.Find("/Resources/BulletBonus"), 5);
        ObjectPool.CreatePool("shield", GameObject.Find("/Resources/Shield"), 5);
               
        //cloudstones
        ObjectPool.CreatePool("blocker_01", GameObject.Find("/Resources/Blockers/Blocker_01"), 10);
        ObjectPool.CreatePool("blocker_02", GameObject.Find("/Resources/Blockers/Blocker_02"), 10);
        ObjectPool.CreatePool("blocker_03", GameObject.Find("/Resources/Blockers/Blocker_03"), 10);
        ObjectPool.CreatePool("blocker_04", GameObject.Find("/Resources/Blockers/Blocker_04"), 10);
        ObjectPool.CreatePool("blocker_05", GameObject.Find("/Resources/Blockers/Blocker_05"), 10);
        ObjectPool.CreatePool("blocker_06", GameObject.Find("/Resources/Blockers/Blocker_06"), 10);

        ObjectPool.CreatePool("lightning_wall_01", GameObject.Find("/Resources/LightningWall_01"), 5);
        ObjectPool.CreatePool("lightning_wall_02", GameObject.Find("/Resources/LightningWall_02"), 5);
        ObjectPool.CreatePool("lightning_wall_03", GameObject.Find("/Resources/LightningWall_03"), 5);
        //enemies
        ObjectPool.CreatePool("hellspawn_enemy_01", GameObject.Find("/Resources/Enemies/Enemy_01"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_02", GameObject.Find("/Resources/Enemies/Enemy_02"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_03", GameObject.Find("/Resources/Enemies/Enemy_03"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_04", GameObject.Find("/Resources/Enemies/Enemy_04"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_05", GameObject.Find("/Resources/Enemies/Enemy_05"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_06", GameObject.Find("/Resources/Enemies/Enemy_06"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_07", GameObject.Find("/Resources/Enemies/Enemy_07"), 5);
        ObjectPool.CreatePool("hellspawn_enemy_08", GameObject.Find("/Resources/Enemies/Enemy_08"), 5);
        //hellspawn bullets
        ObjectPool.CreatePool("hellspawn_bullets_0", GameObject.Find("/Resources/HellSpawnBullets/Bullets_0"), 50);
        //player bullets
        ObjectPool.CreatePool("player_bullets_0", GameObject.Find("/Resources/PlayerBullets/Bullets_0"), 50);        
    }
}
