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

    private int _intMeat;
    private int _intCarb;
    private int _intDiary;
    private int _intVeg;

    [Header("Ingredients")]
    [SerializeField] private int _numberOfIngredients;
    [SerializeField] private GameObject _ingredientObjectsContainer;
    [Header("Lists")]
    [SerializeField] private List<GameObject> _childsObjects;
    [SerializeField] private List<FoodLoader> _childsFoodLoader;
    [SerializeField] private List<Image> _childType;
    [SerializeField] private List<Image> _childPoints;
    [SerializeField] private List<Image> _childBurgerType;

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
            _childsFoodLoader.Add(child.GetComponent<FoodLoader>());
            _childType.Add(child.transform.GetChild(1).gameObject.GetComponent<Image>());
            _childPoints.Add(child.transform.GetChild(2).gameObject.GetComponent<Image>());
            _childBurgerType.Add(child.transform.GetChild(3).gameObject.GetComponent<Image>());
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
        CalculateNumberAndAdjustImageOfTypes();
    }
    private void CalculateNumberAndAdjustImageOfTypes()
    {
        int ingredientIterator = 0;
        foreach (FoodLoader ingredient in _childsFoodLoader)
        {
            if (ingredient.Type == FoodType.Carb)
            {
                _intCarb++;
                _childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/carb");
            }
            else if (ingredient.Type == FoodType.Meat)
            {
                _intMeat++;
                _childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/meat");
            }
            else if (ingredient.Type == FoodType.Vegetable)
            {
                _intVeg++;
                _childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/vegan");
            }    
            else if (ingredient.Type == FoodType.Dairy)
            {
                _intDiary++;
                _childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/dairy");
            }
            else
            {
                _childType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/NO_Meat");
            }
            //------------------
            if (ingredient.points == 1)
            {
                _childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/valor1");
            }
            else if (ingredient.points == 2)
            {
                _childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/valor2");
            }
            else if (ingredient.points == 3)
            {
                _childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/valor3");
            }
            else
            {
                _childPoints[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/NO_Vegan");
            }
            //------------------
            if (ingredient.BurgerType == BurguerType.Bread)
            {
                _childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/breadType");
            }
            else if (ingredient.BurgerType == BurguerType.Hamburguer)
            {
                _childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/hamburgerType");
            }
            else if (ingredient.BurgerType == BurguerType.Topping)
            {
                _childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/toppingType");
            }
            else if (ingredient.BurgerType == BurguerType.Sauce)
            {
                _childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/sauceType");
            }
            else
            {
                _childBurgerType[ingredientIterator].sprite = Resources.Load<Sprite>("Sprites/NO_carb");
            }

            ingredientIterator++;
        }
        _numberOfMeat.text = "x" + _intMeat;
        _numberOfCarb.text = "x" + _intCarb;
        _numberOfDairy.text = "x" + _intDiary;
        _numberOfVeg.text = "x" + _intVeg;
    }

    private void CloseList()
    {
        this.gameObject.SetActive(false);
    }
}
