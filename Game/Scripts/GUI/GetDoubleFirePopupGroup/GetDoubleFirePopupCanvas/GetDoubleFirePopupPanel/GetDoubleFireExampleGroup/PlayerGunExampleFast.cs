using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerGunExampleFast : MonoBehaviour {
    public GameObject bulletsContainer;
    public GameObject playerGunPosition;

    private List<string> bulletNames = new List<string>();

    private float qBulletsDelay = 0.5f;

    private bool _isSpeedUpBonus = true;

    private Sequence _fireBulletSequence;

    void Awake()
    {
        bulletNames.Add("player_bullets_0");

        float _interval = qBulletsDelay;
        if (_isSpeedUpBonus) {
            _interval = qBulletsDelay / 2.0f;
        }

        _fireBulletSequence = DOTween.Sequence();
        _fireBulletSequence.SetLoops(-1);
        _fireBulletSequence.AppendCallback(FireBullet);
        _fireBulletSequence.AppendInterval(_interval);
        _fireBulletSequence.Pause();
    }

    private void Start()
    {
        FireStart();
    }

    void Update()
    {
    }

    public void FireStart()
    {
        _fireBulletSequence.Play();        
    }

    public void FireStop()
    {
        _fireBulletSequence.Kill();
    }

    public void FirePause()
    {
        _fireBulletSequence.Pause();
    }

    private void FireBullet()
    {
        GameObject bullet = GetRandomBullet();
        Vector3 bulletPosition = playerGunPosition.transform.position;
        bulletPosition.z = bulletPosition.z + 0.1f;
        bullet.transform.position = bulletPosition;
        bullet.transform.parent = bulletsContainer.transform;
        bullet.SendMessage("StartMovingExample");        
    }

    private GameObject GetRandomBullet()
    {
        string randomBulletName = bulletNames[Random.Range(0, bulletNames.Count)];
        return ObjectPool.Spawn(randomBulletName); 
    }

    public void OnEnable()
    {
        FireStart();
    }

    public void OnDisable()
    {
        FirePause();
    }
}
