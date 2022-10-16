using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckMaster : MonoBehaviour
{
    public List<Card> cardLibrary;
    List<Card> playerDeck, playerHand;
    public List<FoodId> playerInitalDeck;
    // Start is called before the first frame update
    void Start()
    {
        BuildInitalGameDeck();
        //Draw3Cards();
    }

    public void BuildInitalGameDeck()
    {
        playerDeck = new List<Card>();
        playerHand = new List<Card>();
        foreach (FoodId item in playerInitalDeck)
        {
            playerDeck.Add(cardLibrary.FirstOrDefault(c => c._foodId == item));
            Debug.Log(playerDeck.Last()._foodId);
        }
        playerDeck.Sort();
    }


    public void Draw3Cards()
    {
        Debug.Log(playerDeck.Last()._foodId);
    }

    public FoodId[] PresentNewCardsChoice()
    {
        FoodId[] cardsToPresent = new FoodId[3];
        for (int i = 0; i < 3; i++) { 
            int rand = Random.Range(0, System.Enum.GetValues(typeof(FoodId)).Length);
            cardsToPresent[i] = (FoodId)rand;
        }
        return cardsToPresent;
    }

    public void AddSelectedCardToDeck(FoodId cardId)
    {
        playerDeck.Add(cardLibrary.FirstOrDefault(c => c._foodId == cardId));
    }





    public void BuildInitalGameDeckRandom()
    {
        for (int i = 0; i < 10; i++)
        {
            FoodType food = (FoodType)Random.Range(1, 4);
            //Food
            // _gameDeck.Add(new Card((FoodType(Mathf.));
        }
    }
}
