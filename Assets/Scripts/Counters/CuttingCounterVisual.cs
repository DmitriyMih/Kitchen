using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;
    private const string Cut = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (cuttingCounter != null)
            cuttingCounter.OnCut += ContainerCounterOnCut;
    }

    private void ContainerCounterOnCut(object sender, System.EventArgs e)
    {
        if (animator != null)
            animator.SetTrigger(Cut);
    }
}