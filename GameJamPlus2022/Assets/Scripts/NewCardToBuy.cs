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

        //SpriteRenderer sprite = Resources.Load<Sprite>("Sprites/valor" + myCard._points.ToString());
        myPoints.sprite = Resources.Load<Sprite>("Sprites/valor" + myCard._points.ToString());
        //myPoints = Resources.Load<Image>("Sprites/valor" + myCard._points.ToString());

        //myIcon = Resources.Load<Image>("Sprites/"+LoadMyIcon());
        myIcon.sprite = Resources.Load<Sprite>("Sprites/"+LoadMyIcon());
        myTitle.text = myCard._name;
    }

    string LoadMyIcon()
    {
        string toLoad;
        if (myCard._foodType == FoodType.Meat)
            toLoad = "meat";
        else if (myCard._foodType == FoodType.Vegetable)
            toLoad = "vegan";
        else if (myCard._foodType == FoodType.Dairy)
            toLoad = "dairy";
        else if (myCard._foodType == FoodType.Carb)
            toLoad = "carb";
        else
            toLoad = "meat";

        return toLoad;
    }

    public void OnSelect()
    {
        DeckMaster.instance.AddSelectedCardToDeck(myCard._foodId);
        LevelEndScreen.instance.OnCardSelectedHideMyself();

        
    }
}
