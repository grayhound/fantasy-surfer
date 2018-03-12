using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    public AudioClip mainMenuMusic;
    public AudioClip gameOverMusic;
    public AudioClip[] gameMusic;

    public AudioSource bgmAudioSource;

    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }

    public void UnpauseBGM()
    {
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void PlayMainMenuMusic()
    {
        StopBGM();
        bgmAudioSource.clip = mainMenuMusic;
        bgmAudioSource.Play();
    }

    public void PlayGameOverMusic()
    {
        StopBGM();
        bgmAudioSource.clip = gameOverMusic;
        bgmAudioSource.Play();
    }

    public void PlayRandomGameMusic()
    {
        StopBGM();
        int gameMusicIndex = Random.Range(0, gameMusic.Length);
        bgmAudioSource.clip = gameMusic[gameMusicIndex];
        bgmAudioSource.Play();
    }
}
