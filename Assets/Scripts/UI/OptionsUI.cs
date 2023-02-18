using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private GameObject contentPanel;

    [Space()]
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField, Range(0, 10)] private int soundVolume;
    [SerializeField, Range(0, 10)] private int musicVolume;

    private void Awake()
    {
        Instance = this;

        if (soundEffectsButton != null)
            soundEffectsButton.onClick.AddListener(() =>
            {
                if (SoundManager.Instance != null)
                    SoundManager.Instance.ChangeVolume();
            });
        else Debug.LogError($"{gameObject.name} | Sound Effects Button | Is Null" % Colorize.Yellow % FontFormat.Bold);

        if (musicButton != null)
            musicButton.onClick.AddListener(() =>
            {
                if (MusicManager.Instance != null)
                    MusicManager.Instance.ChangeVolume();
            });
        else Debug.LogError($"{gameObject.name} | Music Button | Is Null" % Colorize.Yellow % FontFormat.Bold);

        if (closeButton != null)
            closeButton.onClick.AddListener(() => Hide());
        else Debug.LogError($"{gameObject.name} | Close Button | Is Null" % Colorize.Yellow % FontFormat.Bold);

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

    public void Show()
    {
        if (contentPanel != null)
            contentPanel.SetActive(true);
        else Debug.LogError($"{gameObject.name} | Content Panel | Is Null" % Colorize.Yellow % FontFormat.Bold);
    }

    public void Hide()
    {
        if (contentPanel != null)
            contentPanel.SetActive(false);
        else Debug.LogError($"{gameObject.name} | Content Panel | Is Null" % Colorize.Yellow % FontFormat.Bold);
    }

    private void UpdateSoundVisualText(int volume)
    {
        if (soundEffectsText != null)
            soundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(volume * 10);
    }

    private void UpdateMusicVisualText(int volume)
    {
        if (musicText != null)
            musicText.text = "MUSIC: " + Mathf.Round(volume * 10);
    }
}