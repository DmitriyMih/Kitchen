using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private RecipeTemplate recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(DeliveryManager.Instance!=null)
        {
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        }

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        if (DeliveryManager.Instance != null)
        {
            foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
            {
                RecipeTemplate recipeTemp = Instantiate(recipeTemplate, container);
                recipeTemp.gameObject.SetActive(true);
                recipeTemp.SetRecipeSO(recipeSO);
            }
        }
    }
}