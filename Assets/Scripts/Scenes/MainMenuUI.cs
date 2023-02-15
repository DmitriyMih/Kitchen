using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        if (playButton == null) Debug.LogError("Play Button Is NUll");
        else
            playButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));

        if (quitButton == null) Debug.LogError("QUit Button Is NUll");
        else
            quitButton.onClick.AddListener(() => Application.Quit());
    }
}