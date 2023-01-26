using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuList : MonoBehaviour
{
    [Header("TextBoxes")]
    [SerializeField] private TextMeshProUGUI _numberOfMeat;
    [SerializeField] private TextMeshProUGUI _numberOfCarb;
    [SerializeField] private TextMeshProUGUI _numberOfDairy;
    [SerializeField] private TextMeshProUGUI _numberOfVeg;

    [SerializeField] private TextMeshProUGUI _numberOfBread;
    [SerializeField] private TextMeshProUGUI _numberOfBurger;
    [SerializeField] private TextMeshProUGUI _numberOfTopping;
    [SerializeField] private TextMeshProUGUI _numberOfSauce;

    private int _intMeat;
    private int _intCarb;
    private int _intDiary;
    private int _intVeg;

    private int _intBread;
    private int _intBurger;
    private int _intTopping;
    private int _intSauce;

    [Header("Ingredients")]
    [SerializeField] private int _numberOfIngredients;
    [SerializeField] private GameObject _ingredientObjectsContainer;
    [Header("Lists")]
    [SerializeField] private List<GameObject> _childsObjects;
    [HideInInspector] public List<FoodLoader> childsFoodLoader;
    [HideInInspector] public List<Image> childType;
    [HideInInspector] public List<Image> childPoints;
    [HideInInspector] public List<Image> childBurgerType;

    [Header("Button")]
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        for (int childNumber = 0; childNumber < _numberOfIngredients; childNumber++)
        {
            _childsObjects.Add(_ingredientObjectsContainer.transform.GetChild(childNumber).gameObject);
        }

        int childIterator = 0;
        foreach (GameObject child in _childsObjects)
        {
            childsFoodLoader.Add(child.GetComponent<FoodLoader>());
            childType.Add(child.transform.GetChild(1).gameObject.GetComponent<Image>());
            childPoints.Add(child.transform.GetChild(2).gameObject.GetComponent<Image>());
            childBurgerType.Add(child.transform.GetChild(3).gameObject.GetComponent<Image>());
            childIterator++;
        }

        
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(CloseList);
    }
    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(CloseList);
    }
    private void Start()
    {
        _intMeat = 0;
        _intCarb = 0;
        _intDiary = 0;
        _intVeg = 0;
        _intBread = 0;
        _intBurger = 0;
        _intTopping = 0;
        _intSauce = 0;
        CalculateNumberAndAdjustImageOfTypes();
    }
    private void CalculateNumberAndAdjustImageOfTypes()
    {
        int ingredientIterator = 0;
        foreach (FoodLoader ingredient in childsFoodLoader)
        {
            if (ingredient.Type == FoodType.Carb)
            {
                _intCarb++;
                childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/carb");
            }
            else if (ingredient.Type == FoodType.Meat)
            {
                _intMeat++;
                childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/meat");
            }
            else if (ingredient.Type == FoodType.Vegetable)
            {
                _intVeg++;
                childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/vegan");
            }    
            else if (ingredient.Type == FoodType.Dairy)
            {
                _intDiary++;
                childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/dairy");
            }
            else
            {
                childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/NO_Meat");
            }
            //------------------
            if (ingredient.points == 1)
            {
                childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/valor1");
            }
            else if (ingredient.points == 2)
            {
                childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/valor2");
            }
            else if (ingredient.points == 3)
            {
                childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/valor3");
            }
            else
            {
                childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/NO_Vegan");
            }
            //------------------
            if (ingredient.BurgerType == BurguerType.Bread)
            {
                _intBread++;
                childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/breadType");
            }
            else if (ingredient.BurgerType == BurguerType.Hamburguer)
            {
                _intBurger++;
                childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/hamburgerType");
            }
            else if (ingredient.BurgerType == BurguerType.Topping)
            {
                _intTopping++;
                childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/toppingType");
            }
            else if (ingredient.BurgerType == BurguerType.Sauce)
            {
                _intSauce++;
                childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/sauceType");
            }
            else
            {
                childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/NO_carb");
            }

            ingredientIterator++;
        }
        _numberOfMeat.text = "x" + _intMeat;
        _numberOfCarb.text = "x" + _intCarb;
        _numberOfDairy.text = "x" + _intDiary;
        _numberOfVeg.text = "x" + _intVeg;
        _numberOfBread.text = "x" + _intBread;
        _numberOfBurger.text = "x" + _intBurger;
        _numberOfTopping.text = "x" + _intTopping;
        _numberOfSauce.text = "x" + _intSauce;
    }

    private void CloseList()
    {
        this.gameObject.SetActive(false);
    }
}
