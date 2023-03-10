using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [SerializeField] private int volume;
    public int Volume
    {
        get => volume;
        set
        {
            volume = value;
            //Debug.Log("Invoke Music");
            if (audioSource != null)
                audioSource.volume = volume / 10f;

            OnMusicVolumeChanged?.Invoke(volume);
            SaveManager.SaveMusicValue(value);
        }
    }

    public Action<int> OnMusicVolumeChanged;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void ChangeVolume()
    {
        if (Volume >= 10)
            Volume = 0;
        else
            Volume += 1;
    }
}