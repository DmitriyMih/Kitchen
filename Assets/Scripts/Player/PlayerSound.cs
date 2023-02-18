using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerController playerController;

    private float footstepTimer;
    [SerializeField] private float footstepTimerMax = 0.1f;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    [SerializeField] private LayerFootstesSoundSO layerFootstesSoundSO;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!playerController.IsWalking || !playerController.IsGrounded)
            return;

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            footstepTimer = footstepTimerMax;
            AudioClip audioClip = GetAudioClip(playerController.CurrentGroundLayerMask);

            if (SoundManager.Instance != null && audioClip != null)
                SoundManager.Instance.PlayFootstepsSound(audioClip, transform.position, volume);
        }
    }

    private AudioClip GetAudioClip(LayerMask layerMask = default)
    {
        if (layerFootstesSoundSO == null)
            return null;

        LayerSound layerSound = new LayerSound();
        bool isFind = false;

        for (int i = 0; i < layerFootstesSoundSO.layerSounds.Count; i++)
        {
            if (layerMask == layerFootstesSoundSO.layerSounds[i].layerMask)
            {
                isFind = true;
                layerSound = layerFootstesSoundSO.layerSounds[i];
                break;
            }
        }

        if (!isFind)
            layerSound = layerFootstesSoundSO.defaultLayerSound;

        //Debug.Log($"Layer mask {layerMask } | Is Find - {isFind} | Layer Sound Mask {layerSound.layerMask}");
        if (layerSound == null) { Debug.LogError("Layer Sound Not Find"); return null; }
        AudioClip audioClip = layerSound.layerFootstepsSound[Random.Range(0, layerSound.layerFootstepsSound.Count)];

        if (audioClip == null) Debug.LogError($"Audio Clip In");

        return audioClip;
    }
}