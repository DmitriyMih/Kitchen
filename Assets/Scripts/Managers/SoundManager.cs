using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClepRefsSO audioClipRefsSO;

    [SerializeField] private int volume;
    public int Volume
    {
        get => volume;
        set
        {
            volume = value;
            //Debug.Log("Invoke Sound");
            OnSoundVolumeChanged?.DynamicInvoke(volume);
            SaveManager.SaveSoundValue(value);
        }
    }

    public Action<int> OnSoundVolumeChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CuttingCounter.OnAnyCut += Instance_OnAnyCut;
        PlayerController.OnPickedSomething += Instance_OnPickedSomething;
        BaseCounter.OnAnyObjectplacedHere += BaseCounter_OnAnyObjectplacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;

        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSucces;
            DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        }
        else
            Debug.LogError("Delievry Manager Is Null");
    }

    public void PlayFootstepsSound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClip, position, volume);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        Vector3 position = trashCounter == null ? Camera.main.transform.position : trashCounter.transform.position;
        PlaySound(audioClipRefsSO.trash, position);
    }

    private void BaseCounter_OnAnyObjectplacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        Vector3 position = baseCounter == null ? Camera.main.transform.position : baseCounter.transform.position;
        PlaySound(audioClipRefsSO.objectDrop, position);
    }

    private void Instance_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlayerController playerCounter = sender as PlayerController;
        Vector3 position = playerCounter == null ? Camera.main.transform.position : playerCounter.transform.position;
        PlaySound(audioClipRefsSO.objectPickup, position);
    }

    private void Instance_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        Vector3 position = cuttingCounter == null ? Camera.main.transform.position : cuttingCounter.transform.position;
        PlaySound(audioClipRefsSO.chop, position);
    }

    private void Instance_OnRecipeSucces(object sender, System.EventArgs e)
    {
        Vector3 position = DeliveryCounter.Instance == null ? Camera.main.transform.position : DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipRefsSO.deliverySuccess, position);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        Vector3 position = DeliveryCounter.Instance == null ? Camera.main.transform.position : DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipRefsSO.deliveryFail, position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * (this.volume / 10f));
    }

    public void ChangeVolume()
    {
        if (Volume >= 10)
            Volume = 0;
        else
            Volume += 1;
    }
}