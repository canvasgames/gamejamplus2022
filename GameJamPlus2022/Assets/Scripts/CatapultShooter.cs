using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultShooter : MonoBehaviour
{
    public static CatapultShooter instance;
    [SerializeField] Animator animator;
    [SerializeField] GameObject food;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    void Shoot()
    {
        Debug.Log("Shoot");
        animator.SetTrigger("Shoot");

    }

    public void DeployFood()
    {
        Debug.Log("Deploy food");
        food.SetActive(true);
    }
}
