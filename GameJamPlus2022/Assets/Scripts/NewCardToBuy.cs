using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class NewCardToBuy : MonoBehaviour
{
    // Start is called before the first frame update
    Card myCard;
    //public Image myPoints;
    public Image myGraphic;
    public Image myIcon;
    public Image myBurgerType;
    public TextMeshProUGUI myTitle;
    public TextMeshProUGUI myScore;


    void Start()
    {
        //StartCoroutine(Initmyself());
    }

    IEnumerator Initmyself()
    {
        yield return new WaitForSeconds(1f);
        initMyCard(FoodId.MysteryBurger);

    }

    public void initMyCard(FoodId myFoodId)
    {
        myCard = DeckMaster.instance.cardLibrary.FirstOrDefault(c => c._foodId == myFoodId);


        var prefab = CatapultShooter.instance.GetPrefabById(myFoodId);
        if (prefab == null)
        {
            Debug.LogError(" Prefab not found for " + myFoodId);
        }
        myGraphic.sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;

        //myPoints.sprite = Resources.Load<Sprite>("Sprites/valor" + myCard._points.ToString());
        myIcon.sprite = GetFoodTypeSprite(myCard._foodType);
        myBurgerType.sprite = GetBurgerTypeSprite(myCard._burguerType);
        myTitle.text = myCard._name;
        myScore.text = myCard._points.ToString();

}

    public void OnSelect()
    {
        DeckMaster.instance.AddSelectedCardToDeck(myCard._foodId);
        DeckMaster.instance.AddSelectedCardToInitialDeck(myCard._foodId);
        LevelEndScreen.instance.OnCardSelectedHideMyself();


    }

    //TODO move to a another class
    public static Sprite GetFoodTypeSprite(FoodType type)
    {
        var spriteFileName = GetFoodTypeSpriteName(type);
        if (string.IsNullOrEmpty(GetFoodTypeSpriteName(type)))
            return null;
        return Resources.Load<Sprite>("Sprites/" + spriteFileName);
    }

    static string GetFoodTypeSpriteName(FoodType type) => type switch
    {
        FoodType.Meat => "meat",
        FoodType.Vegetable => "vegan",
        FoodType.Carb => "carb",
        FoodType.Dairy => "dairy",
        _ => null,
    };

    public static Sprite GetFoodScoreSprite(int score)
    {
        return Resources.Load<Sprite>("Sprites/valor" + score);
    }

    public static Sprite GetBurgerTypeSprite(BurguerType type)
    {
        var spriteFileName = GetBurgerTypeSpriteName(type);
        if (string.IsNullOrEmpty(GetBurgerTypeSpriteName(type)))
            return null;
        return Resources.Load<Sprite>("Sprites/" + spriteFileName + "Type");
    }

    static string GetBurgerTypeSpriteName(BurguerType type) => type switch
    {
        BurguerType.Bread => "bread",
        BurguerType.Hamburguer => "hamburger",
        BurguerType.Sauce => "sauce",
        BurguerType.Topping => "topping",
        _ => null,
    };
}
