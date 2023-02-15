using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static int targetSceneIndex;
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        if (SceneManager.sceneCountInBuildSettings == 0) { Debug.LogError("Scene Count - 0"); return; }
        Loader.targetScene = targetScene;

        Debug.Log("Load - load Scene");
        SceneManager.LoadScene(FindSceneIndex(Scene.LoadingScene));
    }

    public static void Load(int targetSceneIndex)
    {
        if (SceneManager.sceneCountInBuildSettings >= targetSceneIndex) { Debug.LogError("Scene Count - 0"); return; }
        
        SceneManager.LoadScene(targetSceneIndex);
    }

    public static void LoaderCallback()
    {
        Debug.Log("Callback");
        Debug.Log("Load " + targetScene.ToString());
        SceneManager.LoadScene(FindSceneIndex(targetScene));
    }

    private static int FindSceneIndex(Scene scene)
    {
        int index = 0;
        switch (scene)
        {
            default:
                index = 0;
                Debug.LogError($"Scene | {scene.ToString()} | Not Find Index");
                break;

            case Scene.MainMenuScene:
                index = 0;
                break;

            case Scene.LoadingScene:
                index = SceneManager.sceneCountInBuildSettings < 2 ? index = LogError(scene) : 1;
                break;

            case Scene.GameScene:
                index = SceneManager.sceneCountInBuildSettings < 3 ? index = LogError(scene) : 2;
                break;
        }

        Debug.Log("Find - " + index);
        return index;
    }

    private static int LogError(Scene scene)
    {
        Debug.LogError($"Scene | {scene.ToString()} | Not Find | Scene Count - {SceneManager.sceneCountInBuildSettings}");
        return 0;
    }
}