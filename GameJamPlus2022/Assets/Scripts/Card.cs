using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum PrimaryType
{
    Meat = 0,
    Vegetable = 1,
    Strange = 2,
    Cursed = 3
}

public enum SecondaryType
{
    Sauce = 0,
    
}

public class Card : MonoBehaviour
{
    private string _name;
    private string _description;
    private int _points;
    private PrimaryType _type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string[] ReadCardFromTxt()//Ler tipos, nome e pontos
    {
        TextAsset data = Resources.Load("ListOfCards") as TextAsset;
        string card = data.text.Replace(Environment.NewLine, string.Empty);
        string[] cards = card.Split(',');
        //atribuir cada separação de virgula a um atributo da carta
        return null; //botei null pq me perdi no raciocinio, a ideia era ler uma carta da lista em um txt
    }

}
