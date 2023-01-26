using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckMaster : MonoBehaviour
{
    [SerializeField] private Button _shufleButton;
    
    public static DeckMaster instance;
    public List<Card> cardLibrary;
    public List<Card> playerDeck, playerHand, playerDiscard;
    public List<FoodId> playerInitalDeck;
    public bool IsOver;
    public bool NeedToRefill;
    [SerializeField] private TMP_Text textScore;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        BuildInitalGameDeck();
        //BuildInitalGameDeckRandom();

        _shufleButton.onClick.AddListener(ShuffleHand);
    }

    public void BuildInitalGameDeck()
    {
        playerDeck = new List<Card>();
        playerHand = new List<Card>();
        playerDiscard = new List<Card>();
        foreach (FoodId item in playerInitalDeck)
        {
            playerDeck.Add(cardLibrary.FirstOrDefault(c => c._foodId == item));
            // if (playerDeck.Count == 1) return;//TODO Remove
            Debug.Log(playerDeck.Last()._foodId);
        }
        Debug.Log("deck size" + playerDeck.Count);
        //playerDeck
        for (int i = 0; i < 5; i++)
        {
            //RandomItems.Add(AllItems[random.Next(0, AllItems.Count + 1)]);
        }
    }

    #region TURN STUFF
    public void Draw3Cards()
    {
        var total = Mathf.Min(3, playerDeck.Count);
        for (int i = 0; i < total; i++)
        {
            playerHand.Add(playerDeck.Last());
            playerDeck.RemoveAt(playerDeck.Count - 1);
        }
        Debug.Log($"Deck total {playerDeck.Count} Discard total {playerDiscard.Count} Hand total {playerHand.Count}");
    }

    public void ThrowCardsInTheTrash(int selectIndex)
    {
        var total = Mathf.Min(3, playerHand.Count);
        for (int i = 0; i < total; i++)
            if (i != selectIndex)
            {
                playerDiscard.Add(playerHand[i]);
                playerHand[i].flyCounter++;
                Debug.Log(" TRASHH " + playerHand[i]._foodId);
            }
        playerHand.Clear();
    }

    public void CheckDeck()
    {
        if (playerDeck.Count >= 3)
            return;

        if (playerDeck.Count == 0 && playerDiscard.Count == 0)
        {
            IsOver = true;
            return;
        }

        NeedToRefill = true;
        ReshufleDiscard();
    }

    public void ReshufleDiscard()
    {
        Debug.Log(" ReshufleDiscard ");
        foreach (Card trashIngredients in playerDiscard)
        {
            trashIngredients.fromTrash = true;
        }
            
        playerDeck.AddRange(playerDiscard);
        playerDiscard.Clear();
        playerDeck.Sort((c1, c2) => Random.Range(-1, 2));
        SoundController.instance.RandomIngredients();
    }

    public void ShuffleHand()
    {
        FoodSelector.instance.animator.SetTrigger("TrashAll");
    }

    public void ShuffleHandAfterTrashAnimation()
    {
        var total = Mathf.Min(3, playerHand.Count);
        for (int i = 0; i < total; i++)
        {

            playerDiscard.Add(playerHand[i]);
            playerHand[i].flyCounter++;
            Debug.Log(" TRASH SHUFFLE " + playerHand[i]._foodId);
        }
        playerHand.Clear();

        CheckDeck();
        Draw3Cards();
        if (NeedToRefill)
            FoodSelector.instance.ShufleAndPrepareNewOptions();
        else
            FoodSelector.instance.PrepareNewOptions();
        NeedToRefill = false;
    }

    #endregion

    #region LEVEL ENDED

    public FoodId[] PresentNewCardsChoice()
    {
        FoodId[] cardsToPresent = new FoodId[3];
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, System.Enum.GetValues(typeof(FoodId)).Length);
            cardsToPresent[i] = (FoodId)rand;
        }
        return cardsToPresent;
    }

    public void AddSelectedCardToDeck(FoodId cardId)
    {
        playerDeck.Add(cardLibrary.FirstOrDefault(c => c._foodId == cardId));
    }

    #endregion

    public void BuildInitalGameDeckRandom()
    {
        playerDeck = new List<Card>();
        playerHand = new List<Card>();
        playerDiscard = new List<Card>();

        for (int i = 0; i < 10; i++)
        {
            FoodType food = (FoodType)Random.Range(0, 5);
            BurguerType burguer = (BurguerType)Random.Range(0, 4);
            FoodId id = (FoodId)Random.Range(0, 20);

            playerDeck.Add(new Card(food, burguer, 2, id));
        }
    }

    public void CalculateTotalBurguerScore()
    {

    }

    public int CalculateItemScore(Card card)
    {
        int currentFlyCounter = 0;
        if (ClientMaster.instance.CheckIfThisFoodTypeIsUnpleasant(card._foodType))
        {
            textScore.color = Color.red;
            if (card.fromTrash)
            {
                currentFlyCounter = card.flyCounter;
                card.flyCounter = 0;
            }
            return (int)((card._points + currentFlyCounter * ScoreController.instance.flyMultiplier) * ScoreController.instance.restrictionMultiplier);
        }
        else
        {
            if (card.fromTrash)
            {
                currentFlyCounter = card.flyCounter;
                card.flyCounter = 0;
            }
            textScore.color = Color.white;
            return card._points + currentFlyCounter;
        }
    }

    public void ResetFlies()
    {
        foreach (Card card in cardLibrary)
        {
            card.flyCounter = 0;
        }
    }
}
