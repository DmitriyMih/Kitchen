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

    private void Awake()
    {
        if (soundEffectsButton != null)
            soundEffectsButton.onClick.AddListener(() =>
            {
                if (SoundManager.Instance != null)
                    SoundManager.Instance.ChangeVolume();

                UpdateVisualText();
            });

        if (musicButton != null)
            musicButton.onClick.AddListener(() =>
            {

            });
    }

    private void UpdateVisualText()
    {
        if (soundEffectsText != null)
            soundEffectsText.text = "SOUND EFFECTS: " + Mathf.Round(SoundManager.Instance.GetVolume() * 100f);
    }
}