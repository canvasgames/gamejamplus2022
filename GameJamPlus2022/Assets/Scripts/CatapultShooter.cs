using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

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
    [SerializeField] TextMesh foodScore;
    public FoodLoader[] foodPrefabs;
    public BurguerType typeDeployed; 

    bool isShooting;
    float xPosTarget;
    Card card;
    FoodLoader lastFood;
    int spriteSortOrder;

    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.C))
            ClearTable();
    }

    public void PrepareToShoot(Card card)
    {
        this.gameObject.SetActive(true);
        this.card = card;
        var prefab = GetPrefabById(card._foodId);
        foodSpriteRenderer.sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
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

    public void ClearTable()
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
        var prefab = GetPrefabById(card._foodId);
        
        MarkBurgerTypesUsed.instance.MarkType(card._burguerType);
        lastFood = GameObject.Instantiate(prefab, foodPool);
        lastFood.transform.position = spawnPivot.transform.position + Vector3.right * xPosTarget;
        lastFood.GetComponentInChildren<SpriteRenderer>().sortingOrder = ++spriteSortOrder;
        var bodies = lastFood.GetComponentsInChildren<Rigidbody2D>();
        lastFood.AddScoreText(foodScore);
        if (lastFood.CompareTag("Not-Food"))
        {
            SoundController.instance.RandomCongratulations();
        }
        else if (lastFood.CompareTag("Food"))
        {
            SoundController.instance.RandomComplain();
        }
        foreach (var body in bodies)
            body.AddForce(Vector2.down * 600);
            //body.velocity *= Vector2.down * 30;
        Invoke(nameof(OnFoodStopped), 0.8f);
    }

    void OnFoodStopped()
    {
        isShooting = false;
        foodSpriteRenderer.transform.localPosition = Vector3.zero;
        animatorCamera.SetTrigger("Back");
        this.gameObject.SetActive(false);
        RoundController.instance.AfterShoot(card, lastFood);
    }

    public FoodLoader GetPrefabById(FoodId id)
    {
        return foodPrefabs.FirstOrDefault(p => p.GetComponent<FoodLoader>().Id == id);
    }
}
