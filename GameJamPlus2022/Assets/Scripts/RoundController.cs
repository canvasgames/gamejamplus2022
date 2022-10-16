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
        DeckMaster.instance.Draw3Cards();
        var selectedIds = new FoodId[3];
        for (int i = 0; i < DeckMaster.instance.playerHand.Count; i++)
            selectedIds[i] = DeckMaster.instance.playerHand[i]._foodId;

        FoodSelector.instance.PrepareNewOptions(selectedIds);
    }
}
