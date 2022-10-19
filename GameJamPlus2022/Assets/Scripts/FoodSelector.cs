using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodSelector : MonoBehaviour
{
    public static FoodSelector instance;

    Animator animator;
    Button[] buttons;
    Card[] cards;
    Card selected;
    Image[] foodTypeIcons;
    public TextMeshProUGUI DeckSizeText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        buttons = this.GetComponentsInChildren<Button>();
        animator = this.GetComponent<Animator>();
        foodTypeIcons = new Image[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(() => OnOptionSelected(index));
            foodTypeIcons[i] = buttons[i].transform.GetChild(1).GetComponent<Image>();
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
            var sprite = NewCardToBuy.GetFoodTypeSprite(cards[i]._foodType);
            if (sprite == null)
                foodTypeIcons[i].gameObject.SetActive(false);
            else
                foodTypeIcons[i].sprite = sprite;
            buttons[i].gameObject.SetActive(true);
        }
        animator.SetTrigger("Show");
        Invoke(nameof(ShowFoodTypes), 0.75f);
    }

    void ShowFoodTypes()
    {
        for (int i = 0; i < buttons.Length; i++)
            foodTypeIcons[i].gameObject.SetActive(true);
    }

    void OnOptionSelected(int index)
    {
        if (selected != null) return;

        Debug.Log("Selected " + index);
        for (int i = 0; i < buttons.Length; i++)
            foodTypeIcons[i].gameObject.SetActive(false);
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
