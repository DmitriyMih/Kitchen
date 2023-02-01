using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Connect Settings")]
    [SerializeField] private Animator animatior;
    [SerializeField] private PlayerController playerController;

    [Header("Settings")]
    private const string IsWalking = "IsWalking";
    [SerializeField] private bool isWalking;

    private void Awake()
    {
        animatior = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        //playerController.moveEvent += ChangeMoveState;
    }

    private void ChangeMoveState(bool isWalking)
    {
        if (this.isWalking == isWalking)
            return;

        this.isWalking = isWalking;
        if (animatior != null)
            animatior.SetBool(IsWalking, isWalking);
    }
}