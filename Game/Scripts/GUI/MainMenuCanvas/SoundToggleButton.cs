using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggleButton : MonoBehaviour {
    public MainAudioToggler mainAudioToggler;

    public GameObject isOnImage;
    public GameObject isOffImage;

    private bool isEnabled = true;

    void Start()
    {
        UpdateButtonImagesState();
    }

    public void OnClickAction()
    {
        mainAudioToggler.ToggleSfxState();
        UpdateButtonImagesState();
    }

    public void UpdateButtonImagesState()
    {
        isEnabled = mainAudioToggler.GetSfxState();

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
