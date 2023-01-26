using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoundController : MonoBehaviour
{
    public static RoundController instance;
    public List<FoodLoader> Ingredients;
    [SerializeField] GameObject explosion;
    ClientOrder[] clientOrders;

    public int numberOfNextOrders;
    public const int TOTAL_INGREDIENTS = 4;

    private void Awake()
    {
        instance = this;
        clientOrders = this.gameObject.GetComponentsInChildren<ClientOrder>();
    }

    public void StartRoundLevel(int currentLevel)
    {
        numberOfNextOrders = currentLevel+1;
        for (int i = 0; i < clientOrders.Count(); i++)
        {
            if (i <= ClientMaster.instance.GetLevelClients(currentLevel).Count - 1)
            {
                clientOrders[i].gameObject.SetActive(true);
                clientOrders[i].RunAnimation(ClientMaster.instance.GetLevelClients(currentLevel)[i]);
            }
            else
            {
                clientOrders[i].gameObject.SetActive(false);
            }
        }
          
        ShowClientOrder();
        PrepareNewRound();
    }

    public void AfterShoot(Card card, FoodLoader food)
    {
        explosion.SetActive(false);
        AddIngredient(card, food);
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
        ScoreController.instance.UpdateTargetScore();
    }

    public bool IsRoundOver => Ingredients.Count >= TOTAL_INGREDIENTS;

    void AddIngredient(Card card, FoodLoader food)
    {
        Ingredients.Add(food);
        var points = DeckMaster.instance.CalculateItemScore(card);
        food.SetFoodScore(points);
        ScoreController.instance.AddScore(points);
    }

    void EndClientRound()
    {
        Debug.Log("EndClientRound");
        ShowScore();
    }
    
    void ShowScore()
    {
        for (int i = Ingredients.Count - 1; i >= 0; i--)
            Ingredients[i].ShowFoodScore(i);
        if (MarkBurgerTypesUsed.instance.CheckComplete())
        {
            ScoreController.instance.AddForCompletedBurger();
            //mostrar visualmente que o bonus apareceu
        }
        Invoke(nameof(DeliveryOrder), 5f);
    }

    void DeliveryOrder()
    {
        explosion.SetActive(true);
        Invoke(nameof(ClearTable), 0.2f);
    }
    
    void ClearTable()
    {
        SoundController.instance.RandomCostumers();
        ScoreController.instance.UpdateTargetScore();
        CatapultShooter.instance.ClearTable();
        MarkBurgerTypesUsed.instance.ClearAllMarks();
        Ingredients.Clear();
        Invoke(nameof(StartNextClient), 0.2f);
    }

    void StartNextClient()
    {
        clientOrders[ClientMaster.instance.currentClientIndex].MarkAsDone();
        ClientMaster.instance.NextClient();
        if (ClientMaster.instance.currentClientIndex == 0)
            return;
        ShowClientOrder();
        PrepareNewRound();
    }

    void ShowClientOrder()
    {
        //clientOrders[ClientMaster.instance.currentClientIndex].RunAnimation(ClientMaster.instance.currentClient);
    }
}
