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
    Animator animator;

    public int Score;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
        targetScore.text = $"Need: {ClientMaster.instance.targetLevelScore[ClientMaster.instance.currentLevel].ToString()}";
    }
}
