using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            selectedIds[i] = -1;

        for (int i = 0; i < selectedIds.Length; i++)
        {
            var selected = Random.Range(0, CatapultShooter.instance.foodPrefabs.Length);
            while (selectedIds.Contains(selected))
                selected = Random.Range(0, CatapultShooter.instance.foodPrefabs.Length);
            selectedIds[i] = selected;
        }
        FoodSelector.instance.PrepareNewOptions(selectedIds);
    }
}
