using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultContainer : MonoBehaviour
{
    void Ended()
    {
        CatapultShooter.instance.DeployFood();
    }
}
