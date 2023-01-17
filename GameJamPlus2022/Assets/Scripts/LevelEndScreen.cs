using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelEndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI playerScore, levelTargetScore;

    public NewCardToBuy card1, card2, card3;
    public GameObject cardsToBuyMenu;
    public static LevelEndScreen instance;

    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(StartCardBuyAnimations());

    }
    IEnumerator initiii()
    {
        yield return new WaitForSeconds(0.5f);
        //Init();
    }

    public void Init()
    {
        this.gameObject.SetActive(true);
        playerScore.text = ScoreController.instance.Score.ToString();
        levelTargetScore.text = ClientMaster.instance.GetLevelTargetScore().ToString();
        cardsToBuyMenu.SetActive(false);
        StartCoroutine(StartCardBuyAnimations());
    }

    IEnumerator StartCardBuyAnimations()
    {

        yield return new WaitForSeconds(1.5f);
        DisplayCardOptions();
    }

    public void DisplayCardOptions()
    {
        cardsToBuyMenu.SetActive(true);
        FoodId[] cardsToPresent = DeckMaster.instance.PresentNewCardsChoice();
        card1.initMyCard(cardsToPresent[0]);
        card2.initMyCard(cardsToPresent[1]);
        card3.initMyCard(cardsToPresent[2]);
    }


    public void OnCardSelectedHideMyself()
    {
        this.gameObject.SetActive(false);
        //RoundController.instance.StartRoundLevel();
        ClientMaster.instance.NextLevel();
    }

}
