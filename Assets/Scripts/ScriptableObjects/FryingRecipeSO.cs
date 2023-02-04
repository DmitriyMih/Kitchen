using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;

    [Space]
    [SerializeField] private string outputName;

    [ContextMenu("Set Name")]
    private void SetName()
    {
        outputName = input.objectName.ToString() + " - " + output.objectName.ToString();
    }
}