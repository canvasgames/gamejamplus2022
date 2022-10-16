using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoundController : MonoBehaviour
{
    public static RoundController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PrepareNewRound();
    }

    // Update is called once per frame
    public void PrepareNewRound()
    {
        DeckMaster.instance.CheckDeck();
        if (DeckMaster.instance.IsOver)
        {
            Debug.Log("Acabou o jogo");
            return;
        }

        if (DeckMaster.instance.NeedToRefill)
            DeckMaster.instance.ReshufleDiscard();

        DeckMaster.instance.Draw3Cards();
        FoodSelector.instance.PrepareNewOptions();
    }
}
