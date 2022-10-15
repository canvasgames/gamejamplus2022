using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultContainer : MonoBehaviour
{
    [SerializeField] GameObject food;

    void Ended()
    {
        CatapultShooter.instance.DeployFood();
    }
}
