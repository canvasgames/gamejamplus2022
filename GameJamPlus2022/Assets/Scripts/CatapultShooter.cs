using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultShooter : MonoBehaviour
{
    public static CatapultShooter instance;

    [SerializeField] SpriteRenderer foodSpriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] Animator animatorCamera;
    [SerializeField] Transform spawnPivot;
    [SerializeField] float spawnRangeX;
    [SerializeField] float animationTime;
    [SerializeField] Transform foodPool;
    GameObject[] foodPrefabs;

    //Bugado
    [Header("Prefabs")]
    [SerializeField] GameObject unityLixo1;
    [SerializeField] GameObject unityLixo2;
    [SerializeField] GameObject unityLixo3;

    bool isShooting;
    float xPosTarget;
    int foodIndex;

    private void Awake()
    {
        instance = this;
        foodPrefabs = new GameObject[]
        {
            unityLixo1, unityLixo2, unityLixo3
        };
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (Input.GetKeyDown(KeyCode.C))
            ClearTable();
    }

    public void PrepareToShoot(int foodIndex)
    {
        this.gameObject.SetActive(true);
        this.foodIndex = foodIndex;
        foodSpriteRenderer.sprite = foodPrefabs[foodIndex].GetComponent<SpriteRenderer>().sprite;
    }

    void Shoot()
    {
        if (isShooting) return;

        Debug.Log("Shoot");
        animator.SetTrigger("Shoot");
        animatorCamera.SetTrigger("Shoot");
        isShooting = true;
        xPosTarget = spawnPivot.position.x + Random.Range(-spawnRangeX, spawnRangeX);
        StartCoroutine(AnimateXAxis());
    }

    void ClearTable()
    {
        var total = foodPool.childCount;
        for (int i = total - 1; i >= 0; i--)
            Destroy(foodPool.GetChild(i).gameObject);
    }

    IEnumerator AnimateXAxis()
    {
        var currAnimTime = 0f;
        while (currAnimTime < animationTime)
        {
            currAnimTime += Time.deltaTime;
            var lerp = currAnimTime / animationTime;
            foodSpriteRenderer.transform.localPosition = Vector3.right * (xPosTarget * lerp);
            yield return new WaitForEndOfFrame();
        }
    }

    public void DeployFood()
    {
        Debug.Log("Deploy food");
        var food = GameObject.Instantiate(foodPrefabs[foodIndex], foodPool);
        food.transform.position = spawnPivot.transform.position + Vector3.right * xPosTarget;
        Invoke("OnFoodStopped", 0.5f);
    }

    void OnFoodStopped()
    {
        isShooting = false;
        foodSpriteRenderer.transform.localPosition = Vector3.zero;
        animatorCamera.SetTrigger("Back");
        this.gameObject.SetActive(false);
        FoodSelector.instance.PrepareNewRound();
    }
}
