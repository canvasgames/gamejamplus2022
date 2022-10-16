using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoundController : MonoBehaviour
{
    public static RoundController instance;
    public List<Card> Ingredients;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PrepareNewRound();
    }

    public void AfterShoot(Card card)
    {
        AddIngredient(card);
        if (IsRoundOver || DeckMaster.instance.IsOver)
        {
            Invoke(nameof(EndClientRound), 0.5f);
            return;
        }
        PrepareNewRound();
    }

    void PrepareNewRound()
    {
        DeckMaster.instance.CheckDeck();
        DeckMaster.instance.Draw3Cards();
        if (DeckMaster.instance.NeedToRefill)
            FoodSelector.instance.ShufleAndPrepareNewOptions();
        else
            FoodSelector.instance.PrepareNewOptions();
        DeckMaster.instance.NeedToRefill = false;
    }

    public bool IsRoundOver => Ingredients.Count >= 5;

    void AddIngredient(Card card)
    {
        Ingredients.Add(card);
        var points = DeckMaster.instance.CalculateItemScore(card);
        ScoreController.instance.AddScore(points);
    }

    void EndClientRound()
    {
        Debug.Log("Acabou o jogo");
        explosion.SetActive(true);
        Invoke(nameof(ClearTable), 0.2f);
    }

    void ClearTable()
    {
        CatapultShooter.instance.ClearTable();
        Ingredients.Clear();
        ;//TODO next client
        PrepareNewRound();
    }
}
