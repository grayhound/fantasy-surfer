using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {
    public static string PLAYER_TAG = "Player";
    public static string ENEMY_HELLSPAWN_TAG = "HellSpawnEnemy";

    public static string PLAYER_PREFS_LIVES_SAVENAME = "PlayerMaxLives";
    public static string PLAYER_PREFS_LIVES_BONUS_GAMES_SAVENAME = "PlayerLivesBonusGames";
    public static int PLAYER_DEFAULT_MAX_LIVES = 3;
    public static int PLAYER_MAX_POSSIBLE_LIVES = 6;
    public static int PLAYER_MAX_POSSIBLE_LIVES_BONUS_GAMES = 5;

    public static float SHIELD_BONUS_TIME = 10.0f;

    public GameObject player_alternative_sprite_position;
    public Game game;
    public GameObject playerLivesCanvas;
    public GameObject[] playerLiveContainers;

    public SpriteRenderer[] playerChildSprites;

    public SFX sfx;

    public PlayerGun playerGun;

    public GameObject heroAuraShield;

    public PlayerBonusStatus playerBonusStatus;

    public Collider2D[] childColliders;

    private int playerMaxLives = 3;
    private int playerLives = 3;
    private int playerLivesBonusGames = 0;

    private Rigidbody2D playerRigidBody;
    private SpriteRenderer playerSpriteRenderer;

    private float _blinkDeadPlayerInterval = 0.3f;

    private Sequence _shieldBonusSequence;
    private Sequence _autoShieldSequence;

    private bool _shieldBonus = false;

    public static string PLAYER_PREFS_AUTO_SHIELD_BONUS_GAMES_SAVENAME = "PlayerAutoShieldBonusGames";
    public static int PLAYER_MAX_POSSIBLE_AUTO_SHIELD_BONUS_GAMES = 5;
    private int autoShieldBonusGames = 0;

    private bool _isDying = false;

    private bool isAlwaysMaxLives = false;
    private bool isAlwaysAutoShield = false;
    
    void Start ()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        LoadPlayerLivesBonus();
        LoadAutoShieldBonus();
        SetStartPosition();
    }

    public void SetStartPosition()
    {
        Vector3 start_position = gameObject.transform.position;
        start_position.x = player_alternative_sprite_position.transform.position.x;
        start_position.y = player_alternative_sprite_position.transform.position.y;
        gameObject.transform.position = start_position;
    }

    public void AddDeath()
    {
        sfx.PlaySfxPlayerDeath();
        if (_shieldBonus) {
            return;
        }
        if (_isDying) {
            return;
        }
        _isDying = true;
        playerLives--;
        if (playerLives < 0) {
            DisableChildColliders();
            game.GameOver();
        } else {
            RunDieAnimation();
            game.PauseGameDeath();
        }
        
    }

    public void RunDieAnimation()
    {
        playerRigidBody.bodyType = RigidbodyType2D.Kinematic;
        UpdatePlayerLiveContainers();        
        BlinkDeadPlayer();
    }

    public void ResetPlayer()
    {
        _isDying = false;
        StopPlayer();
        SetStartPosition();
        EnableChildColliders();
        playerLives = GetPlayerMaxLives();
        playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
        UpdatePlayerLiveContainers();
        playerGun.ResetPlayerGun();
        StartAutoShield();        
    }

    private int GetPlayerMaxLives()
    {
        if (isAlwaysMaxLives) {
            return PLAYER_MAX_POSSIBLE_LIVES;
        } else {
            return playerMaxLives;
        }
    }

    public void EnableMaxLives()
    {
        isAlwaysMaxLives = true;
    }

    public void DisableMaxLives()
    {
        isAlwaysMaxLives = false;
    }    
    

    public void StopPlayer()
    {        
        StopShieldBonus();
        StopAutoShield();
    }

    public void Ressurect()
    {        
        playerRigidBody.bodyType = RigidbodyType2D.Dynamic;
        game.UnpauseGameDeath();
        _isDying = false;
    }

    private void BlinkDeadPlayer()
    {
        Sequence blinkSequence = DOTween.Sequence();
        foreach (SpriteRenderer playerChildSprite in playerChildSprites) {
            blinkSequence = DOTween.Sequence();
            _addBlinkToBlinkSequence(blinkSequence, playerChildSprite);
            _addBlinkToBlinkSequence(blinkSequence, playerChildSprite);
            _addBlinkToBlinkSequence(blinkSequence, playerChildSprite);
            _addBlinkToBlinkSequence(blinkSequence, playerChildSprite);
            _addBlinkToBlinkSequence(blinkSequence, playerChildSprite);            
        }
        blinkSequence.AppendCallback(Ressurect);
        //blinkSequence.Play();
    }    

    public void UpdatePlayerLiveContainers()
    {
        int maxDisplayHearts = playerLiveContainers.Length;
        int displayHearts = playerLives;
        if (displayHearts > maxDisplayHearts) {
            displayHearts = maxDisplayHearts;
        }

        for (int i = 0; i < displayHearts; i++) {
            playerLiveContainers[i].SetActive(true);
        }
        for (int i = displayHearts; i < maxDisplayHearts; i++) {
            playerLiveContainers[i].SetActive(false);
        }
    }

    public void ShowPlayerLiveContainers()
    {
        playerLivesCanvas.SetActive(true);
    }

    public void HidePlayerLiveContainers()
    {
        playerLivesCanvas.SetActive(false);
    }

    public void AddLifeBonus()
    {
        playerLivesBonusGames = 5;
        AddMaxPlayerLife();
    }

    public void RemoveLifeBonus()
    {
        playerLivesBonusGames--;
        if (playerLivesBonusGames <= 0) {
            playerLivesBonusGames = 0;
        }
        SavePlayerLives();
    }

    public void CheckBonusGames()
    {
        if (playerLivesBonusGames <= 0) {
            playerMaxLives = PLAYER_DEFAULT_MAX_LIVES;
        }        
    }

    public void AddMaxPlayerLife()
    {
        playerMaxLives++;
        if (playerMaxLives >= PLAYER_MAX_POSSIBLE_LIVES) {
            playerMaxLives = PLAYER_MAX_POSSIBLE_LIVES;
        }
        SavePlayerLives();
    }

    private void LoadPlayerLivesBonus()
    {
        int _maxLives = PlayerPrefs.GetInt(PLAYER_PREFS_LIVES_SAVENAME, PLAYER_DEFAULT_MAX_LIVES);
        int _bonusGames = PlayerPrefs.GetInt(PLAYER_PREFS_LIVES_BONUS_GAMES_SAVENAME, 0);        
        if (_maxLives >= PLAYER_MAX_POSSIBLE_LIVES) {
            _maxLives = PLAYER_MAX_POSSIBLE_LIVES;
        }
        if (_bonusGames >= PLAYER_MAX_POSSIBLE_LIVES_BONUS_GAMES) {
            _bonusGames = PLAYER_MAX_POSSIBLE_LIVES_BONUS_GAMES;
        }
        playerMaxLives = _maxLives;
        playerLivesBonusGames = _bonusGames;
    }

    private void SavePlayerLives()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_LIVES_SAVENAME, playerMaxLives);
        PlayerPrefs.SetInt(PLAYER_PREFS_LIVES_BONUS_GAMES_SAVENAME, playerLivesBonusGames);
    }

    private void _addBlinkToBlinkSequence(Sequence blinkSequence, SpriteRenderer playerChildSprite)
    {
        blinkSequence.Append(playerChildSprite.DOFade(0, 0));
        blinkSequence.AppendInterval(_blinkDeadPlayerInterval);
        blinkSequence.Append(playerChildSprite.DOFade(1, 0));
        blinkSequence.AppendInterval(_blinkDeadPlayerInterval);
    }

    public int GetMaxPlayerLives()
    {
        return playerMaxLives;
    }

    public int GetPlayerLivesBonusGames()
    {
        return playerLivesBonusGames;
    }

    public void PausePlayer()
    {
        DisableChildColliders();
        _shieldBonusSequence.Pause();
        _autoShieldSequence.Pause();
    }

    public void UnpausePlayer()
    {
        EnableChildColliders();
        _shieldBonusSequence.Play();
        _autoShieldSequence.Play();
    }

    public void StartShieldBonus()
    {
        sfx.PlaySfxPowerupShield();
        StopShieldBonus();
        _shieldBonus = true;        
        _shieldBonusSequence = DOTween.Sequence();
        _shieldBonusSequence.AppendInterval(SHIELD_BONUS_TIME);
        _shieldBonusSequence.AppendCallback(StopShieldBonus);
        //_shieldBonusSequence.Play();
        heroAuraShield.SetActive(true);
        playerBonusStatus.StartBonusShield(SHIELD_BONUS_TIME);
    }

    public void StopShieldBonus()
    {
        _shieldBonus = false;
        _shieldBonusSequence.Kill();
        heroAuraShield.SetActive(false);
        playerBonusStatus.StopBonusShield();        
        StartAutoShield();
    }

    /********************************************************************/
    public void AddAutoShieldBonus()
    {
        autoShieldBonusGames = 5;
        SaveAutoShieldBonus();
    }

    public void LoadAutoShieldBonus()
    {
        int _bonusGames = PlayerPrefs.GetInt(PLAYER_PREFS_AUTO_SHIELD_BONUS_GAMES_SAVENAME, 0);        
        if (_bonusGames >= PLAYER_MAX_POSSIBLE_AUTO_SHIELD_BONUS_GAMES) {
            _bonusGames = PLAYER_MAX_POSSIBLE_AUTO_SHIELD_BONUS_GAMES;
        }
        autoShieldBonusGames = _bonusGames;
    }

    public int GetAutoShieldBonusGames()
    {
        return autoShieldBonusGames;
    }

    public void RemoveAutoShieldBonus()
    {
        autoShieldBonusGames--;
        if (autoShieldBonusGames <= 0) {
            autoShieldBonusGames = 0;
        }
        SaveAutoShieldBonus();
    }

    public void SaveAutoShieldBonus()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_AUTO_SHIELD_BONUS_GAMES_SAVENAME, autoShieldBonusGames);
    }

    /********************************************************************/
    public void StartAutoShield()
    {        
        if (autoShieldBonusGames <= 0 && !isAlwaysAutoShield) {
            return;
        }
        StopAutoShield();
        _autoShieldSequence = DOTween.Sequence();
        _autoShieldSequence.AppendInterval(30.0f);
        _autoShieldSequence.AppendCallback(StartShieldBonus);
    }
    
    public void EnableAlwaysAutoshield()
    {
        isAlwaysAutoShield = true;
    }

    public void DisableAlwaysAutoshield()
    {
        isAlwaysAutoShield = false;
    }     

    public void StopAutoShield()
    {        
        _autoShieldSequence.Kill();
    }

    private void EnableChildColliders()
    {
        for (int i = 0; i < childColliders.Length; i++) {
            childColliders[i].enabled = true;
        }
    }    
    
    private void DisableChildColliders()
    {
        for (int i = 0; i < childColliders.Length; i++) {
            childColliders[i].enabled = false;
        }
    }
    
}
