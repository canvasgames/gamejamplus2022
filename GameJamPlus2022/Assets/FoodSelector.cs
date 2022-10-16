using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSelector : MonoBehaviour
{
    public static FoodSelector instance;

    Animator animator;
    Button[] buttons;
    int[] ids;
    int selected;

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

    public void PrepareNewOptions(int[] selectedIds)
    {
        selected = -1;
        ids = selectedIds;
        for (int i = 0; i < buttons.Length; i++)
        {
            var prefab = CatapultShooter.instance.foodPrefabs[selectedIds[i]];
            buttons[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            buttons[i].gameObject.SetActive(true);
        }
        animator.SetTrigger("Show");
    }

    void OnOptionSelected(int i)
    {
        if (selected != -1) return;

        Debug.Log("Selected " + i);
        selected = ids[i];
        animator.SetTrigger("Trash");
        for (int j = 0; j < buttons.Length; j++)
            buttons[j].gameObject.SetActive(i != j);
        Invoke(nameof(PrepareToShoot), 1f);
    }

    void PrepareToShoot()
    {
        CatapultShooter.instance.PrepareToShoot(selected);

    }
}
