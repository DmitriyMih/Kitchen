using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private const string OpenClose = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (containerCounter != null)
            containerCounter.OnPlayerGrabbedObject += ContainerCounterOnPlayerGradientObject;
    }

    private void ContainerCounterOnPlayerGradientObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OpenClose);
    }
}