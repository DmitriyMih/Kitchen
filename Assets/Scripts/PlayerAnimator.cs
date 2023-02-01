using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animatior;
    private PlayerController player;

    private const string IsWalking = "IsWalking";

    private void Awake()
    {
        animatior = GetComponent<Animator>();
    }


}