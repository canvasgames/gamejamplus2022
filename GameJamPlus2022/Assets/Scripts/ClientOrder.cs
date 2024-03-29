using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientOrder : MonoBehaviour
{
    public static ClientOrder instance;
    [SerializeField] SpriteRenderer foodTypeSpriteRenderer;
    [SerializeField] SpriteRenderer mark;
    [SerializeField] float animationSpeed;
    Vector3 startPos;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        startPos = this.transform.localPosition;
    }

    public void RunAnimation(ClientType clientType)
    {
        foodTypeSpriteRenderer.sprite = GetSpriteByClientType(clientType);
        this.gameObject.SetActive(true);
        StartCoroutine(Animation());
    }
    public void MarkAsDone() => mark.gameObject.SetActive(true);

    public Sprite GetSpriteByClientType(ClientType clientType) => Resources.Load<Sprite>("Sprites/" + GetSpriteNameByClientType(clientType));
    string GetSpriteNameByClientType(ClientType clientType) => clientType switch
    {
        ClientType.Carnivorous => "NO_Vegan",
        ClientType.Vegan => "NO_Meat",
        ClientType.LowCarb => "NO_carb",
        ClientType.DairyIntolerant => "NO_Dairy",
        ClientType.Random => "question_mark",
        _ => "",
    };

    IEnumerator Animation()
    {
        mark.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        this.transform.localPosition = Vector3.left * 25;
        while (Vector3.SqrMagnitude(this.transform.localPosition - startPos) > 0.05f)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, startPos, animationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        this.transform.localPosition = startPos;
    }
}
