using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSelector : MonoBehaviour
{
    public static FoodSelector instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        var buttons = this.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
        
    }

    public void PrepareNewRound()
    {
        this.gameObject.SetActive(true);
    }

    void OnOptionSelected(int i)
    {
        Debug.Log("Selected " + i);
        CatapultShooter.instance.PrepareToShoot(i);
        this.gameObject.SetActive(false);
    }
}
