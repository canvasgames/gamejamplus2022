using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    [SerializeField] TMP_Text textScore;
    [SerializeField] TMP_Text targetScore;
    [SerializeField] TMP_Text textNewScore;
    [SerializeField] TMP_Text textCountingScore;

    Animator animator;
    TMP_Text[] textCountingScores;

    public int Score;

    [Header("GD")]
    public float restrictionMultiplier;
    public float flyMultiplier;

    private void Awake()
    {
        instance = this;
        //textCountingScores = new TMP_Text[RoundController.TOTAL_INGREDIENTS];
        //textCountingScores[0] = textCountingScore;
        //textCountingScore.gameObject.SetActive(false);
        //for (int i = 1; i < RoundController.TOTAL_INGREDIENTS; i++)
        //    textCountingScores[i] = Instantiate(textCountingScore, textCountingScore.transform.parent);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        textNewScore.text = "";
    }

    // Update is called once per frame
    public void AddScore(int addScore)
    {
        Score += addScore;
        textScore.text = $"Score: {Score}";
        textNewScore.text = $"+{addScore}";
        animator.SetTrigger("Show");
    }

    public void UpdateTargetScore()
    {
        targetScore.text = $"Target: {ClientMaster.instance.targetLevelScore[(ClientMaster.instance.currentLevel)-1]}";
    }

    public void ShowBurgerScore(FoodLoader[] foods, ClientType client)
    {

    }

    public void ResetScore()
    {
        Score = 0;
        textScore.text = "Score: 0";
    }
}
