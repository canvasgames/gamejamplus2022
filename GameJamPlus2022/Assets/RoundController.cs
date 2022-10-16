using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public static RoundController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PrepareNewRound();
    }

    // Update is called once per frame
    public void PrepareNewRound()
    {
        var selectedIds = new int[3];
        for (int i = 0; i < selectedIds.Length; i++)
        {
            selectedIds[i] = Random.Range(0, CatapultShooter.instance.foodPrefabs.Length);
        }
        FoodSelector.instance.PrepareNewOptions(selectedIds);
    }
}
