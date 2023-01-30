using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _shuffleButton;
    [SerializeField] private Button _tryAgain;
    private bool _nextLevel;
    // Start is called before the first frame update
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI levelTargetScore;
    public TextMeshProUGUI resultText;


    public NewCardToBuy card1, card2, card3;
    public GameObject cardsToBuyMenu;
    public static LevelEndScreen instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _shuffleButton.SetActive(false);
        _tryAgain.onClick.AddListener(OnCardSelectedHideMyself);
    }

    private void OnDisable()
    {
        _shuffleButton.SetActive(true);
        _tryAgain.onClick.RemoveListener(OnCardSelectedHideMyself);
    }

    IEnumerator initiii()
    {
        yield return new WaitForSeconds(0.5f);
        //Init();
    }

    public void Init(bool levelComplete)
    {
        gameObject.SetActive(true);
        DeckMaster.instance.ResetFlies();
        playerScore.text = ScoreController.instance.Score.ToString();
        levelTargetScore.text = ClientMaster.instance.GetLevelTargetScore().ToString();
        cardsToBuyMenu.SetActive(false);
        if (levelComplete)
        {
            _tryAgain.gameObject.SetActive(false);
            resultText.text = "\"Acceptable\"\nWork";
            _nextLevel = true;
            StartCoroutine(StartCardBuyAnimations());
        }
        else
        {
            _nextLevel = false;
            resultText.text = "Try\nWorse";
            StartCoroutine(WaitForButtonToShow());
        }
            
    }

    IEnumerator StartCardBuyAnimations()
    {

        yield return new WaitForSeconds(1.5f);
        DisplayCardOptions();
    }

    IEnumerator WaitForButtonToShow()
    {
        yield return new WaitForSeconds(1.5f);
        _tryAgain.gameObject.SetActive(true);
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
        gameObject.SetActive(false);
        //RoundController.instance.StartRoundLevel();
        if (_nextLevel)
        {
            ClientMaster.instance.currentLevel++;
        }
        else
        {
            ClientMaster.instance.currentLevel = 1;
            ScoreController.instance.ResetScore();
        } 
        ClientMaster.instance.InitLevel();
    }
}
