using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeTemplate : MonoBehaviour
{
    [SerializeField] private Transform content;

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Image ingridientImage;

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        ingridientImage.gameObject.SetActive(false);
        recipeNameText.text = recipeSO.name;

        for (int i = 0; i < recipeSO.kitchenObjectSOList.Count; i++)
        {
            Image image = Instantiate(ingridientImage, content);
            image.sprite = recipeSO.kitchenObjectSOList[i].sprite;
            image.gameObject.SetActive(true);
        }
    }
}