using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSelector : MonoBehaviour
{
    public static FoodSelector instance;

    Animator animator;
    Button[] buttons;
    Card[] cards;
    Card selected;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        buttons = this.GetComponentsInChildren<Button>();
        animator = this.GetComponent<Animator>();
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    public void ShufleAndPrepareNewOptions()
    {
        animator.SetTrigger("Shufle");
        Invoke(nameof(PrepareNewOptions), 1);
    }

    public void PrepareNewOptions()
    {
        selected = null;
        cards = DeckMaster.instance.playerHand.ToArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (cards.Length <= i || cards[i] == null)
            {
                buttons[i].gameObject.SetActive(false);
                continue;
            }

            buttons[i].gameObject.SetActive(true);
            var prefab = CatapultShooter.instance.GetPrefabById(cards[i]._foodId);
            if (prefab == null)
            {
                Debug.Log(i + " Prefab not found for " + cards[i]._foodId);
            }
            buttons[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            buttons[i].gameObject.SetActive(true);
        }
        animator.SetTrigger("Show");
    }

    void OnOptionSelected(int i)
    {
        if (selected != null) return;

        Debug.Log("Selected " + i);
        selected = cards[i];     
        DeckMaster.instance.ThrowCardsInTheTrash(i);
        animator.SetTrigger("Trash" + (i + 1));
        Invoke(nameof(PrepareToShoot), 1f);
    }

    void PrepareToShoot()
    {
        CatapultShooter.instance.PrepareToShoot(selected);

    }
}
