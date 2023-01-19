using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIngredientList : MonoBehaviour
{
    [SerializeField] private Button _openListButton;
    [SerializeField] private GameObject _ingredientList;

    private void Awake()
    {
        _openListButton.onClick.AddListener(OpenList);
    }
    private void OpenList()
    {
        _ingredientList.SetActive(true);
    }
}
