using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoundController : MonoBehaviour
{
    public static RoundController instance;
    public List<Card> Ingredients;
    [SerializeField] GameObject explosion;
    ClientOrder[] clientOrders;

    const int TOTAL_INGREDIENTS = 4;

    private void Awake()
    {
        instance = this;
        clientOrders = this.gameObject.GetComponentsInChildren<ClientOrder>();
    }

    public void StartRoundLevel()
    {
        foreach (var hook in clientOrders)
            hook.gameObject.SetActive(false);
        ShowClientOrder();
        PrepareNewRound();
    }

    public void AfterShoot(Card card)
    {
        explosion.SetActive(false);
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

    public bool IsRoundOver => Ingredients.Count >= TOTAL_INGREDIENTS;

    void AddIngredient(Card card)
    {
        Ingredients.Add(card);
        var points = DeckMaster.instance.CalculateItemScore(card);
        ScoreController.instance.AddScore(points);
    }

    void EndClientRound()
    {
        Debug.Log("Client finished");
        explosion.SetActive(true);
        Invoke(nameof(ClearTable), 0.2f);
        SoundController.instance.RandomCostumers();
    }

    void ClearTable()
    {
        CatapultShooter.instance.ClearTable();
        Ingredients.Clear();
        clientOrders[ClientMaster.instance.currentClientIndex].MarkAsDone();
        ClientMaster.instance.NextClient();
        if (ClientMaster.instance.currentClientIndex == 0)
            return;
        ShowClientOrder();
        PrepareNewRound();
    }

    void ShowClientOrder()
    {
        clientOrders[ClientMaster.instance.currentClientIndex].RunAnimation(ClientMaster.instance.currentClient);
    }
}
