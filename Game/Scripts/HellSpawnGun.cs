using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HellSpawnGun : MonoBehaviour
{    
    public GameObject bulletsContainer;

    private List<string> bulletNames = new List<string>();

    private float qDelay = 1.0f;
    private int qBulletsMin = 1;
    private int qBulletsMax = 1;

    private float qBulletsDelay = 0.15f;

    private int qBulletsToFire = 0;
    private int qBulletsFired = 0;

    private Sequence _fireQSequence;
    private Sequence _fireBulletSequence;

    private float _bulletSpeedTimeScale;

    void Start ()
    {
        bulletNames.Add("hellspawn_bullets_0");
	}

    public void ResetGameObject()
    {
        StopFire();
    }

    public void RandomizeQ()
    {
        qDelay = Random.Range(2.0f, 3.5f);
        qBulletsMin = Random.Range(1, 3);
        qBulletsMax = Random.Range(qBulletsMin, 5);
    }

    public void StartFire(float bulletSpeedTimeScale)
    {
        RandomizeQ();
        //qBulletsToFire = Random.Range(qBulletsMin, qBulletsMax);
        qBulletsToFire = 1;
        qBulletsFired = 0;

        _bulletSpeedTimeScale = bulletSpeedTimeScale;
        
        FireSequence();                
    }

    public void UpdateFire(float bulletSpeedTimeScale)
    {        
        _bulletSpeedTimeScale = bulletSpeedTimeScale;
    }    

    public void StopFire()
    {
        _fireQSequence.Kill();
    }

    public void PauseHellSpawnGun()
    {
        _fireQSequence.Pause();
    }

    public void UnpauseHellSpawnGun()
    {
        _fireQSequence.Play();
    }

    private void FireSequence()
    {
        _fireQSequence = DOTween.Sequence();        
        _fireQSequence.AppendCallback(() => FireBullet());
        _fireQSequence.AppendInterval(qDelay);
        _fireQSequence.SetLoops(-1);        
    }

    private void FireBullet()
    {        
        GameObject bullet = GetRandomBullet();
        Vector3 bulletPosition = gameObject.transform.position;
        bulletPosition.z = bulletPosition.z + 1;
        bullet.transform.position = bulletPosition;
        bullet.transform.parent = bulletsContainer.transform;
        bullet.SendMessage("StartMoving", _bulletSpeedTimeScale);

        Spawner.AddSpawnedObject(bullet);

        qBulletsFired++;
    }

    private GameObject GetRandomBullet()
    {
        string randomBulletName = bulletNames[Random.Range(0, bulletNames.Count)];
        return ObjectPool.Spawn(randomBulletName);
    }
}