using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBonusStatus : MonoBehaviour {
    public GameObject playerBonusCanvas;

    /**********************************/
    public GameObject playerBonusShield;
    public Text playerBonusShieldText;
    private int playerBonusShieldTime;
    private Sequence bonusShieldSequence;
    /**********************************/
    public GameObject playerBonusStar;
    public Text playerBonusStarText;
    private int playerBonusStarTime;
    private Sequence bonusStarSequence;
    /**********************************/
    public GameObject playerBonusSpeedup;
    public Text playerBonusSpeedupText;
    private int playerBonusSpeedupTime;
    private Sequence bonusSpeedupSequence;
    /**********************************/
    public GameObject playerBonusBullet;
    public Text playerBonusBulletText;
    private int playerBonusBulletTime;
    private Sequence bonusBulletSequence;
    /**********************************/

    public void EnablePlayerBonusStatus()
    {
        playerBonusCanvas.SetActive(true);
    }

    public void DisablePlayerBonusStatus()
    {
        playerBonusCanvas.SetActive(false);
    }

    /*Shield****************************************/
    public void StartBonusShield(float bonusTime)
    {
        StopBonusShield();
        playerBonusShieldTime = (int)bonusTime;
        UpdateTickBonusShieldText();
        playerBonusShield.SetActive(true);
        TickBonusShieldSequence();
    }

    private void TickBonusShieldSequence()
    {
        bonusShieldSequence = DOTween.Sequence();
        bonusShieldSequence.AppendInterval(1.0f);
        bonusShieldSequence.AppendCallback(TickBonusShield);
    }

    private void TickBonusShield()
    {
        playerBonusShieldTime--;
        if (playerBonusShieldTime < 0) {
            StopBonusShield();
        } else {
            UpdateTickBonusShieldText();
            TickBonusShieldSequence();

        }
    }

    private void UpdateTickBonusShieldText()
    {
        playerBonusShieldText.text = string.Format("{0:00}", playerBonusShieldTime);
    }

    public void StopBonusShield()
    {
        bonusShieldSequence.Kill();
        playerBonusShield.SetActive(false);
    }

    /*Star*****************************************/

    public void StartBonusStar(float bonusTime)
    {
        StopBonusStar();
        playerBonusStarTime = (int)bonusTime;
        UpdateTickBonusStarText();
        playerBonusStar.SetActive(true);
        TickBonusStarSequence();
    }

    private void TickBonusStarSequence()
    {
        bonusStarSequence = DOTween.Sequence();
        bonusStarSequence.AppendInterval(1.0f);
        bonusStarSequence.AppendCallback(TickBonusStar);
    }

    private void TickBonusStar()
    {
        playerBonusStarTime--;
        if (playerBonusStarTime < 0) {
            StopBonusStar();
        } else {
            UpdateTickBonusStarText();
            TickBonusStarSequence();

        }
    }

    private void UpdateTickBonusStarText()
    {
        playerBonusStarText.text = string.Format("{0:00}", playerBonusStarTime);
    }

    public void StopBonusStar()
    {
        bonusStarSequence.Kill();
        playerBonusStar.SetActive(false);
    }

    /*Speedup*************************************/

    public void StartBonusSpeedup(float bonusTime)
    {
        StopBonusSpeedup();
        playerBonusSpeedupTime = (int)bonusTime;
        UpdateTickBonusSpeedupText();
        playerBonusSpeedup.SetActive(true);
        TickBonusSpeedupSequence();
    }

    private void TickBonusSpeedupSequence()
    {
        bonusSpeedupSequence = DOTween.Sequence();
        bonusSpeedupSequence.AppendInterval(1.0f);
        bonusSpeedupSequence.AppendCallback(TickBonusSpeedup);
    }

    private void TickBonusSpeedup()
    {
        playerBonusSpeedupTime--;
        if (playerBonusSpeedupTime < 0) {
            StopBonusSpeedup();
        } else {
            UpdateTickBonusSpeedupText();
            TickBonusSpeedupSequence();

        }
    }

    private void UpdateTickBonusSpeedupText()
    {
        playerBonusSpeedupText.text = string.Format("{0:00}", playerBonusSpeedupTime);
    }

    public void StopBonusSpeedup()
    {
        bonusSpeedupSequence.Kill();
        playerBonusSpeedup.SetActive(false);
    }

    /*BUllet**************************************/
    public void StartBonusBullet(float bonusTime)
    {
        StopBonusBullet();
        playerBonusBulletTime = (int)bonusTime;
        UpdateTickBonusBulletText();
        playerBonusBullet.SetActive(true);
        TickBonusBulletSequence();
    }

    private void TickBonusBulletSequence()
    {
        bonusBulletSequence = DOTween.Sequence();
        bonusBulletSequence.AppendInterval(1.0f);
        bonusBulletSequence.AppendCallback(TickBonusBullet);
    }

    private void TickBonusBullet()
    {
        playerBonusBulletTime--;
        if (playerBonusBulletTime < 0) {
            StopBonusBullet();
        } else {
            UpdateTickBonusBulletText();
            TickBonusBulletSequence();

        }
    }

    private void UpdateTickBonusBulletText()
    {
        playerBonusBulletText.text = string.Format("{0:00}", playerBonusBulletTime);
    }

    public void StopBonusBullet()
    {
        bonusBulletSequence.Kill();
        playerBonusBullet.SetActive(false);
    }
    /*********************************************/

    public void PauseBonusStatus()
    {
        bonusShieldSequence.Pause();
        bonusStarSequence.Pause();
        bonusSpeedupSequence.Pause();
        bonusBulletSequence.Pause();
    }

    public void UnpauseBonusStatus()
    {
        bonusShieldSequence.Play();
        bonusStarSequence.Play();
        bonusSpeedupSequence.Play();
        bonusBulletSequence.Play();
    }
}
