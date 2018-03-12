using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainAudioToggler : MonoBehaviour {
    public static string MAIN_AUDIO_TOGGLER_SFX_STATE_SAVENAME = "SfxEnabled";
    public static string MAIN_AUDIO_TOGGLER_BGM_STATE_SAVENAME = "BgmEnabled";

    public AudioMixer mainAudioMixer;

    private bool sfxEnabled = true;
    private bool bgmEnabled = true;

	void Start ()
    {
        LoadAudioState();
        SetAudioState();
	}
		
	void Update ()
    {		
	}

    public bool ToggleSfxState()
    {
        sfxEnabled = !sfxEnabled;
        SetAudioState();
        SaveAudioState();
        return sfxEnabled;
    }

    public bool ToggleBgmState()
    {
        bgmEnabled = !bgmEnabled;
        SetAudioState();
        SaveAudioState();
        return bgmEnabled;
    }

    public bool GetSfxState()
    {
        return sfxEnabled;
    }

    public bool GetBgmState()
    {
        return bgmEnabled;
    }

    private void LoadAudioState()
    {
        sfxEnabled = PlayerPrefs.GetInt(MAIN_AUDIO_TOGGLER_SFX_STATE_SAVENAME, 1) == 1 ? true : false;
        bgmEnabled = PlayerPrefs.GetInt(MAIN_AUDIO_TOGGLER_BGM_STATE_SAVENAME, 1) == 1 ? true : false;
    }

    private void SaveAudioState()
    {
        PlayerPrefs.SetInt(MAIN_AUDIO_TOGGLER_SFX_STATE_SAVENAME, sfxEnabled ? 1 : 0);
        PlayerPrefs.SetInt(MAIN_AUDIO_TOGGLER_BGM_STATE_SAVENAME, bgmEnabled ? 1 : 0);
    }

    private void SetAudioState()
    {
        if (sfxEnabled) {
            mainAudioMixer.SetFloat("sfxVolume", 0.0f);
        } else {
            mainAudioMixer.SetFloat("sfxVolume", -80.0f);
        }

        if (bgmEnabled) {
            mainAudioMixer.SetFloat("bgmVolume", 0.0f);
        } else {
            mainAudioMixer.SetFloat("bgmVolume", -80.0f);
        }
    }
}
