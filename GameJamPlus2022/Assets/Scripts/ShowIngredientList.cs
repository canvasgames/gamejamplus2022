using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIngredientList : MonoBehaviour
{
    public static ShowIngredientList instance;
    public Button openListButton;
    [SerializeField] private GameObject _ingredientList;

    private void Awake()
    {
        instance = this;
        openListButton.onClick.AddListener(OpenList);
    }
    private void OpenList()
    {
        _ingredientList.SetActive(true);
    }

    public void ActivateListButton()
    {
        openListButton.interactable = true;
    }
    public void DeactivateListButton()
    {
        openListButton.interactable = false;
    }
}
