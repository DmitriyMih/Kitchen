using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Start()
    {
        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSucces;
            DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed; ;
        }
        else
            Debug.LogError("Delievry Manager Is Null");
    }

    private void Instance_OnRecipeSucces(object sender, System.EventArgs e)
    {

    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {

    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

}