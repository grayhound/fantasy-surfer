using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {
    [System.Serializable]
    public struct SfxCollection
    {
        public AudioSource coinPickup;
        public AudioSource bulletFire;
        public AudioSource pauseGame;
        public AudioSource playerDeath;
        public AudioSource powerup;
        public AudioSource powerupShield;
    }

    public SfxCollection sfxCollection;

    public void Start()
    {
    }

    public void PlaySfxCoinPickup()
    {
        sfxCollection.coinPickup.Play();
    }

    public void PlaySfxBulletFire()
    {
        sfxCollection.bulletFire.Play();
    }

    public void PlaySfxPauseGame()
    {
        sfxCollection.pauseGame.Play();
    }

    public void PlaySfxPlayerDeath()
    {
        sfxCollection.playerDeath.Play();
    }

    public void PlaySfxPowerup()
    {
        sfxCollection.powerup.Play();
    }

    public void PlaySfxPowerupShield()
    {
        sfxCollection.powerupShield.Play();
    }
}
