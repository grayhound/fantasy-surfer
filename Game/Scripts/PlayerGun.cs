using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerGun : MonoBehaviour {
    public GameObject bulletsContainer;
    public GameObject playerGunPosition;

    public PlayerBonusStatus playerBonusStatus;

    private List<string> bulletNames = new List<string>();

    private float qBulletsDelay = 0.5f;

    private bool isPaused = false;

    private float _speedUpBonusTime = 10.0f;
    Sequence _speedUpBonusStopSequence;
    private bool _isSpeedUpBonus = false;

    private Sequence _fireBulletSequence;

    private int doubleFireBonusGames = 0;

    public static string PLAYER_PREFS_DOUBLE_FIRE_BONUS_GAMES_SAVENAME = "PlayerDoubleFireBonusGames";
    public static int PLAYER_MAX_POSSIBLE_DOUBLE_FIRE_BONUS_GAMES = 5;

    private bool isAlwaysDoubleFire = false;

    void Start ()
    {
        _speedUpBonusStopSequence = DOTween.Sequence();
        bulletNames.Add("player_bullets_0");
        LoadDoubleFireBonus();
    }

    public void ResetPlayerGun()
    {
        SpeedUpBonusStop();
    }

    public void FireStart()
    {
        FireStop();
        FireBullet();
        
    }

    public void FireStop()
    {
        _fireBulletSequence.Kill();
    }

    public void PauseGun()
    {
        isPaused = true;
        DOTween.Pause(PlayerBullet.PLAYER_BULLET_DOTWEEN_ID);
        _speedUpBonusStopSequence.Pause();
        _fireBulletSequence.Pause();
    }

    public void StopGun()
    {
        FireStop();
        SpeedUpBonusStop();
    }

    public void UnpauseGun()
    {
        isPaused = false;
        DOTween.Play(PlayerBullet.PLAYER_BULLET_DOTWEEN_ID);
        _speedUpBonusStopSequence.Play();
        _fireBulletSequence.Play();
    }

    public void SpeedUpBonus()
    {
        SpeedUpBonusStop();

        _speedUpBonusStopSequence = DOTween.Sequence();
        _speedUpBonusStopSequence.AppendInterval(_speedUpBonusTime);
        _speedUpBonusStopSequence.AppendCallback(SpeedUpBonusStop);
        _isSpeedUpBonus = true;

        playerBonusStatus.StartBonusBullet(_speedUpBonusTime);
    }

    public void SpeedUpBonusStop()
    {
        _speedUpBonusStopSequence.Kill();
        _isSpeedUpBonus = false;
        playerBonusStatus.StopBonusBullet();
    }

    private void FireBullet()
    {
        if (isPaused) {
            return;
        }
        GameObject bullet = GetRandomBullet();
        Vector3 bulletPosition = playerGunPosition.transform.position;
        bulletPosition.z = bulletPosition.z + 1;
        bullet.transform.position = bulletPosition;
        bullet.transform.parent = bulletsContainer.transform;
        bullet.transform.localScale = new Vector3(1, 1, 1);
        bullet.SendMessage("StartMoving");

        Spawner.AddSpawnedObject(bullet);

        float _interval = GetBulletInterval();
        _fireBulletSequence = DOTween.Sequence();
        _fireBulletSequence.AppendInterval(_interval);
        _fireBulletSequence.AppendCallback(FireBullet);        
    }

    private float GetBulletInterval()
    {
        float _interval = qBulletsDelay;
        if (_isSpeedUpBonus) {
            _interval = qBulletsDelay / 2;
        }
        if (doubleFireBonusGames > 0 || isAlwaysDoubleFire) {
            _interval = _interval / 2;
        }
        return _interval;
    }

    public void EnableAlwaysDoubleFire()
    {
        isAlwaysDoubleFire = true;
    }
    
    public void DisableAlwaysDoubleFire()
    {
        isAlwaysDoubleFire = false;
    }    

    private GameObject GetRandomBullet()
    {
        string randomBulletName = bulletNames[Random.Range(0, bulletNames.Count)];
        return ObjectPool.Spawn(randomBulletName);
    }

    public void AddDoubleFireBonus()
    {
        doubleFireBonusGames = 5;
        SaveDoubleFireBonus();
    }

    public void LoadDoubleFireBonus()
    {        
        int _bonusGames = PlayerPrefs.GetInt(PLAYER_PREFS_DOUBLE_FIRE_BONUS_GAMES_SAVENAME, 0);
        if (_bonusGames >= PLAYER_MAX_POSSIBLE_DOUBLE_FIRE_BONUS_GAMES) {
            _bonusGames = PLAYER_MAX_POSSIBLE_DOUBLE_FIRE_BONUS_GAMES;
        }        
        doubleFireBonusGames = _bonusGames;
    }

    public int GetDoubleFireBonusGames()
    {
        return doubleFireBonusGames;
    }

    public void RemoveDoubleFireBonus()
    {
        doubleFireBonusGames--;
        if (doubleFireBonusGames <= 0) {
            doubleFireBonusGames = 0;
        }
        SaveDoubleFireBonus();
    }

    public void SaveDoubleFireBonus()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_DOUBLE_FIRE_BONUS_GAMES_SAVENAME, doubleFireBonusGames);
    }
}
