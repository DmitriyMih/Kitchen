using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;

    [Space]
    [SerializeField] private string outputName;

    [ContextMenu("Set Name")]
    private void SetName()
    {
        outputName = input.objectName.ToString() + " - " + output.objectName.ToString();
    }
}