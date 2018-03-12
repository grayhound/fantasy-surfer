﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggleButton : MonoBehaviour {
    public MainAudioToggler mainAudioToggler;

    public GameObject isOnImage;
    public GameObject isOffImage;

    private bool isEnabled = true;

    public void Start()
    {
        UpdateButtonImagesState();
    }

    public void OnClickAction()
    {
        mainAudioToggler.ToggleBgmState();
        UpdateButtonImagesState();
    }

    public void UpdateButtonImagesState()
    {
        isEnabled = mainAudioToggler.GetBgmState();

        if (isEnabled) {
            isOnImage.SetActive(true);
            isOffImage.SetActive(false);
        } else {
            isOnImage.SetActive(false);
            isOffImage.SetActive(true);
        }
    }

    private void OnEnable()
    {
        UpdateButtonImagesState();
    }
}
