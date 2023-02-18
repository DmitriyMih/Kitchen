using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressObject;
    [SerializeField] private Image fillImage;

    private IHasProgress hasProgress;

    [Header("Bar Settings")]
    [SerializeField] private GameObject barGroup;
    [SerializeField] private float fillingTime = 0.1f;

    private void Start()
    {
        if (hasProgressObject != null)
            hasProgress = hasProgressObject.GetComponent<IHasProgress>();

        if (hasProgress != null)
            hasProgress.OnProgressChanged += HasProgressOnProgressChanged;

        FillBar(0);
        Hide();
    }

    private void HasProgressOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        FillBar(e.progressNormalized);
    }

    private void FillBar(float value, bool instantFill = false)
    {
        if (fillImage == null) { Debug.LogError("Fill Image Is Null"); return; }

        value = Mathf.Clamp(value, 0f, 1f);

        if (instantFill)
            fillImage.DOFillAmount(value, 0);
        else
            fillImage.DOFillAmount(value, fillingTime);

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
        if (fillImage == null) { Debug.LogError("Fill Image Is Null"); return; }

        barGroup.SetActive(false);
        fillImage.fillAmount = 0;
    }
}