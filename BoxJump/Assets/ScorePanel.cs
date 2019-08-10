using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorePanel : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    private int bestScore;
    private int currentScore;
    private void Start()
    {
        currentScore = GameManager.instance.score;
        bestScore = PlayerPrefs.GetInt("BestScore");
        if(bestScore < currentScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            bestScore = currentScore;
        }
        score.text = currentScore.ToString();
        highScore.text = bestScore.ToString();
    }
}
