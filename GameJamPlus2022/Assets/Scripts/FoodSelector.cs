using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodSelector : MonoBehaviour
{
    public static FoodSelector instance;

    public Animator animator;
    Button[] buttons;
    Card[] cards;
    Card selected;
    Image[] foodTypeIcons;
    Image[] foodScore;
    Image[] hamburgerTypeIcons;
    GameObject[] flyIcon;
    TextMeshProUGUI[] flyCounter;
    public TextMeshProUGUI DeckSizeText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        buttons = this.GetComponentsInChildren<Button>();
        animator = this.GetComponent<Animator>();
        foodTypeIcons = new Image[buttons.Length];
        foodScore = new Image[buttons.Length];
        hamburgerTypeIcons = new Image[buttons.Length];
        flyIcon = new GameObject[buttons.Length];
        flyCounter = new TextMeshProUGUI[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(() => OnOptionSelected(index));
            foodTypeIcons[i] = buttons[i].transform.GetChild(1).GetComponent<Image>();
            foodScore[i] = buttons[i].transform.GetChild(2).GetComponent<Image>();
            hamburgerTypeIcons[i] = buttons[i].transform.GetChild(3).GetComponent<Image>();
            flyIcon[i] = buttons[i].transform.GetChild(4).gameObject;
            flyCounter[i] = flyIcon[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

    }

    private void Start()
    {
        DeckSizeText.text = DeckMaster.instance.playerDeck.Count.ToString();
    }

    public void ShufleAndPrepareNewOptions()
    {
        animator.SetTrigger("Shufle");
        Invoke(nameof(PrepareNewOptions), 1);
    }

    public void PrepareNewOptions()
    {
        selected = null;
        DeckSizeText.text = DeckMaster.instance.playerDeck.Count.ToString();

        cards = DeckMaster.instance.playerHand.ToArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (cards.Length <= i || cards[i] == null)
            {
                buttons[i].gameObject.SetActive(false);
                continue;
            }

            var prefab = CatapultShooter.instance.GetPrefabById(cards[i]._foodId);
            if (prefab == null)
            {
                Debug.Log(i + " Prefab not found for " + cards[i]._foodId);
            }
            buttons[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            var typeSprite = NewCardToBuy.GetFoodTypeSprite(cards[i]._foodType);
            var scoreSprite = NewCardToBuy.GetFoodScoreSprite(cards[i]._points);
            var hamburgerTypeSprite = NewCardToBuy.GetBurgerTypeSprite(cards[i]._burguerType);
            if (cards[i].flyCounter > 0)
            {
                flyIcon[i].SetActive(true);
                flyCounter[i].text = "x" + cards[i].flyCounter;
            }
            else
            {
                flyIcon[i].SetActive(false);
            }
            if (typeSprite == null)
            {
                foodTypeIcons[i].gameObject.SetActive(false);
                foodScore[i].gameObject.SetActive(false);
                hamburgerTypeIcons[i].gameObject.SetActive(false);
            }

            else
            {
                foodTypeIcons[i].sprite = typeSprite;
                foodScore[i].sprite = scoreSprite;
                hamburgerTypeIcons[i].sprite = hamburgerTypeSprite;
            }
                

            buttons[i].gameObject.SetActive(true);
        }
        animator.SetTrigger("Show");
        Invoke(nameof(ShowFoodTypes), 0.75f);
    }

    void ShowFoodTypes()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            foodTypeIcons[i].gameObject.SetActive(true);
            foodScore[i].gameObject.SetActive(true);
            hamburgerTypeIcons[i].gameObject.SetActive(true);
        }
            
    }

    void OnOptionSelected(int index)
    {
        if (selected != null) return;

        Debug.Log("Selected " + index);
        for (int i = 0; i < buttons.Length; i++)
        {
            foodTypeIcons[i].gameObject.SetActive(false);
            foodScore[i].gameObject.SetActive(false);
            hamburgerTypeIcons[i].gameObject.SetActive(false);
        }
        
        selected = cards[index];     
        DeckMaster.instance.ThrowCardsInTheTrash(index);
        animator.SetTrigger("Trash" + (index + 1));
        Invoke(nameof(PrepareToShoot), 1f);
    }

    void PrepareToShoot()
    {
        CatapultShooter.instance.PrepareToShoot(selected);

    }
}
