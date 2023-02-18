using UnityEditor;
using UnityEngine;

public static class SaveManager
{
    private const string SoundEffectsValue = "SoundEffectsValue";
    private const string MusicValue = "MusicValue";

    [MenuItem("My Tools / Save System / Clear All Prefs")]
    public static void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SaveSoundValue(int value)
    {
        PlayerPrefs.SetInt(SoundEffectsValue, value);
        PlayerPrefs.Save();
        //Debug.Log($"Save Sound - {value} " % Colorize.Orange % FontFormat.Bold + $"Get {PlayerPrefs.GetInt(SoundEffectsValue)}" % Colorize.Green % FontFormat.Bold);
    }

    public static int LoadSoundValue()
    {
        //Debug.Log($"Load Sound {PlayerPrefs.GetInt(SoundEffectsValue)} " % Colorize.Blue % FontFormat.Bold);
        return PlayerPrefs.GetInt(SoundEffectsValue, 5);
    }

    public static void SaveMusicValue(int value)
    {
        PlayerPrefs.SetInt(MusicValue, value);
        PlayerPrefs.Save();
        //Debug.Log($"Save Music - {value} " % Colorize.Orange % FontFormat.Bold + $"Get {PlayerPrefs.GetInt(MusicValue)}" % Colorize.Green % FontFormat.Bold);
    }

    public static int LoadMusicValue()
    {
        //Debug.Log($"Load Music {PlayerPrefs.GetInt(MusicValue)} " %Colorize.Blue % FontFormat.Bold);
        return PlayerPrefs.GetInt(MusicValue, 5);
    }
}