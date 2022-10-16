using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckMaster : MonoBehaviour
{
    public static DeckMaster instance;
    public List<Card> cardLibrary;
    public List<Card> playerDeck, playerHand, playerDiscard;
    public List<FoodId> playerInitalDeck;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        BuildInitalGameDeck();
        //BuildInitalGameDeckRandom();
    }

    public void BuildInitalGameDeck()
    {
        playerDeck = new List<Card>();
        playerHand = new List<Card>();
        playerDiscard = new List<Card>();
        foreach (FoodId item in playerInitalDeck)
        {
            playerDeck.Add(cardLibrary.FirstOrDefault(c => c._foodId == item));
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
        for (int i = 0; i < 3; i++)
        {
            if (playerDeck.Count == 0)
            {
                //WAIT FOR SHUFFLE ANIMATION;
                ReshufleDiscard();
            }

            playerHand.Add(playerDeck.Last());
            Debug.Log(playerDeck.Last()._foodId + " aa " + playerHand.Last()._foodId);
            playerDeck.RemoveAt(playerDeck.Count - 1);
        }
    }

    public void ThrowCardsInTheTrash(int selectIndex)
    {
        for (int i = 0; i < 3; i++)
            if (i != selectIndex)
            {
                playerDiscard.Add(playerHand.Last());
                Debug.Log(playerHand.Last()._foodId + " TRASHH " + playerDiscard.Last()._foodId);
            }
        playerHand.Clear();
    }

    public void ReshufleDiscard()
    {
        playerDeck = playerDiscard;
        playerDiscard = null;
        playerDiscard = new List<Card>();
        playerDeck.Sort();
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

    public int CalculateItemScore(FoodId id)
    {
        return 5;
    }
}
