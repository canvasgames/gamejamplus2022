using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _shuffleButton;
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

    private void OnEnable()
    {
        _shuffleButton.SetActive(false);
    }

    private void OnDisable()
    {
        _shuffleButton.SetActive(true);
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
        DeckMaster.instance.ResetFlies();
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
        ShowNextOrders.instance.NextOrders();
        FoodId[] cardsToPresent = DeckMaster.instance.PresentNewCardsChoice();
        card1.initMyCard(cardsToPresent[0]);
        card2.initMyCard(cardsToPresent[1]);
        card3.initMyCard(cardsToPresent[2]);
        ScoreController.instance.ResetScore();
        
        DeckMaster.instance.markShuffle.SetActive(false);
    }


    public void OnCardSelectedHideMyself()
    {
        this.gameObject.SetActive(false);
        //RoundController.instance.StartRoundLevel();
        ClientMaster.instance.currentLevel++;
        ClientMaster.instance.InitLevel();
    }

}
