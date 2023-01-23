using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum FoodId
{
    RegularBread,
    CheeseBread,
    MoldyBread,
    Tire,
    VeganBread,
    RegularBurguer,
    RottenBurger,
    MysteryBurger,
    MushroomBurger,
    SoyBurguer,
    Lettuce,
    Newspaper,
    RegularCheese,
    RottenCheese,
    OldSock,
    RadioativeSauce,
    WhiteSauce,
    Ketchup,
    HoneySauce,
    SwampSauce,
}

public enum FoodType
{
    Untyped = 0,
    Meat = 1,
    Vegetable = 2,
    Carb = 3,
    Dairy = 4
}

public enum BurguerType
{
    Bread = 0,
    Hamburguer = 1,
    Topping = 2,
    Sauce = 3,
}

[Serializable]
public class Card 
{
    public FoodId _foodId;
    public string _name;
    private string _description;
    public int _points;
    public FoodType _foodType;
    public BurguerType _burguerType;
    public int flyCounter = 0;
    public bool fromTrash = false;
    

    private string[] ReadCardFromTxt()//Ler tipos, nome e pontos
    {
        TextAsset data = Resources.Load("ListOfCards") as TextAsset;
        string card = data.text.Replace(Environment.NewLine, string.Empty);
        string[] cards = card.Split(',');
        //atribuir cada separa??o de virgula a um atributo da carta
        return null; //botei null pq me perdi no raciocinio, a ideia era ler uma carta da lista em um txt
    }

    public Card (FoodType foodType, BurguerType burguerType, int points, FoodId id)
    {
        _foodType = foodType;
        _burguerType = burguerType;
        _points = points;
        _foodId = id;
    }
}
