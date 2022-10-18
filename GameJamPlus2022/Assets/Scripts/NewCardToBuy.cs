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
    public Image myPoints, myGraphic, myIcon;
    public TextMeshProUGUI myTitle;

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

        myPoints.sprite = Resources.Load<Sprite>("Sprites/valor" + myCard._points.ToString());
        myIcon.sprite = GetFoodTypeSprite(myCard._foodType);
        myTitle.text = myCard._name;
    }

    public void OnSelect()
    {
        DeckMaster.instance.AddSelectedCardToDeck(myCard._foodId);
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
}
