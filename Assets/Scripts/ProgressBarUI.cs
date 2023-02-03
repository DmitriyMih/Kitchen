using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image fillImage;

    [SerializeField] private GameObject barGroup;

    private void Start()
    {
        if (cuttingCounter != null)
            cuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;

        FillBar(0); 
        Hide();
    }

    private void CuttingCounterOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        FillBar(e.progressNormalized);
    }

    private void FillBar(float value)
    {
        if (fillImage == null) { Debug.LogError("Fill Image Is Null"); return; }

        value = Mathf.Clamp(value, 0f, 1f);
        fillImage.fillAmount = value;

        if (value == 0f || value == 1f)
            Hide();
        else
            Show();
    }

    private void Show()
    {
        if (barGroup == null) { Debug.Log("Bar Group Is Null"); return; }
        barGroup.SetActive(true);
    }

    private void Hide()
    {
        if (barGroup == null) { Debug.Log("Bar Group Is Null"); return; }
        barGroup.SetActive(false);
    }
}