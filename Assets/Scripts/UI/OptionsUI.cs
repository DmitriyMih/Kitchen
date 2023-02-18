using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField, Range(0, 10)] private int soundVolume;
    [SerializeField, Range(0, 10)] private int musicVolume;

    private void Awake()
    {
        if (soundEffectsButton != null)
            soundEffectsButton.onClick.AddListener(() =>
            {
                if (SoundManager.Instance != null)
                    SoundManager.Instance.ChangeVolume();
            });

        if (musicButton != null)
            musicButton.onClick.AddListener(() =>
            {
                if (MusicManager.Instance != null)
                    MusicManager.Instance.ChangeVolume();
            });

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.OnSoundVolumeChanged += UpdateSoundVisualText;
            SoundManager.Instance.Volume = soundVolume;
        }
        
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.OnMusicVolumeChanged += UpdateMusicVisualText;
            MusicManager.Instance.Volume = musicVolume;
        }
    }

    private void UpdateSoundVisualText(int volume)
    {
        Debug.Log("Update Sound");
        if (soundEffectsText != null)
            soundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(volume * 10);
    }

    private void UpdateMusicVisualText(int volume)
    {
        Debug.Log("Update Music");
        if (musicText != null)
            musicText.text = "MUSIC: " + Mathf.Round(volume * 10);
    }
}