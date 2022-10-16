using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSelector : MonoBehaviour
{
    public static FoodSelector instance;

    Button[] buttons;
    int[] ids;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        buttons = this.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
        
    }

    public void PrepareNewOptions(int[] selectedIds)
    {
        ids = selectedIds;
        for (int i = 0; i < buttons.Length; i++)
        {
            var prefab = CatapultShooter.instance.foodPrefabs[selectedIds[i]];
            buttons[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
        }
        this.gameObject.SetActive(true);
    }

    void OnOptionSelected(int i)
    {
        Debug.Log("Selected " + i);
        CatapultShooter.instance.PrepareToShoot(ids[i]);
        this.gameObject.SetActive(false);
    }
}
