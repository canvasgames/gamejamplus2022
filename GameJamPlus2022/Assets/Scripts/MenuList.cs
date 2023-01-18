using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfMeat;
    [SerializeField] private TextMeshProUGUI _numberOfCarb;
    [SerializeField] private TextMeshProUGUI _numberOfDairy;
    [SerializeField] private TextMeshProUGUI _numberOfVeg;

    private int _intMeat;
    private int _intCarb;
    private int _intDiary;
    private int _intVeg;

    [SerializeField] private List<GameObject> _childsObjects;
    [SerializeField] private List<FoodLoader> _childsFoodLoader;
    [SerializeField] private List<Image> _childType;

    private void Awake()
    {
        int i = 0;
        foreach(GameObject child in _childsObjects)
        {
            _childsFoodLoader[i] = child.GetComponent<FoodLoader>();
            _childType[i] = child.transform.GetChild(1).gameObject.GetComponent<Image>();
            i++;
        }
    }
    private void Start()
    {
        _intMeat = 0;
        _intCarb = 0;
        _intDiary = 0;
        _intVeg = 0;
        CalculateNumberOfTypes();
    }
    private void CalculateNumberOfTypes()
    {//adicionar aqui a mudança dos tipos dos ingredientes(imagens)
        foreach(FoodLoader ingredient in _childsFoodLoader)
        {
            if (ingredient.Type == FoodType.Carb)
                _intCarb++;
            if (ingredient.Type == FoodType.Meat)
                _intMeat++;
            if (ingredient.Type == FoodType.Vegetable)
                _intVeg++;
            if (ingredient.Type == FoodType.Dairy)
                _intDiary++;

            _numberOfMeat.text = "x"+ _intMeat;
            _numberOfCarb.text = "x"+ _intCarb;
            _numberOfDairy.text = "x"+ _intDiary;
            _numberOfVeg.text = "x"+ _intVeg;
        }
    }
    
}
