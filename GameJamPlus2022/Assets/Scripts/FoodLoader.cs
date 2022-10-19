using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodLoader : MonoBehaviour
{
    public FoodId Id;
    public FoodType Type;
    TextMesh textScore;

    public void AddScoreText(TextMesh prefab)
    {
        textScore = Instantiate(prefab, this.transform);
        textScore.gameObject.SetActive(false);
    }

    public void SetFoodScore(int score) => textScore.text = score.ToString();

    public void ShowFoodScore(int index)
    {
        var posY = this.transform.position.y;
        if (this.transform.childCount == 2)
            posY = this.transform.GetChild(0).transform.position.y;
        else
        {
            posY = this.transform.GetChild(Mathf.RoundToInt(this.transform.childCount / 2)).transform.position.y;
        }
        textScore.transform.SetPositionAndRotation(new Vector2(4, posY), Quaternion.identity);
        textScore.gameObject.SetActive(true);

        var bodies = this.gameObject.GetComponentsInChildren<Rigidbody2D>();
        foreach (var body in bodies)
            body.constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(MoveToDisplayPosition(index));
    }

    public IEnumerator MoveToDisplayPosition(int index)
    {
        //new WaitForSeconds(index);
        var targetXPosition = this.transform.position.x;
        var targetYPosition = ((index + 1) * 1.5f - textScore.transform.position.y) + this.transform.position.y;
        while (Mathf.Abs(targetYPosition - this.transform.position.y) > 0.1)
        {
            this.transform.position = new Vector2(targetXPosition, Mathf.MoveTowards(this.transform.position.y, targetYPosition, 4f * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        this.transform.position = new Vector2(targetXPosition, targetYPosition);
    }
}
