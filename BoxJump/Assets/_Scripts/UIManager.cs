using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private ScoreAnimation scoreAnimation;
    byte r = 255, g = 0, b = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreAnimation = GetComponentInChildren<ScoreAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        scoreText.text = GameManager.instance.score.ToString();
        scoreText.faceColor = NextColor();
    }
    public void ScoreAnimation()
    {
        scoreAnimation.PointAnimation();
    }
    private Color NextColor()
    {
       
        byte delta = 35;
        
            if (r > 0 && b == 0)
            {
                if (r - delta < 0)
                {
                    r = 0;
                    g = 255;
                }
                else
                {
                    r -= delta;
                    g += delta;
                }
                
            }
            if (g > 0 && r == 0)
            {
                if (g - delta < 0)
                {
                     g = 0;
                     b = 255;
                }
            else
            {
                g -= delta;
                b += delta;
            }
            }
            if (b > 0 && g == 0)
            {
                if (b - delta < 0)
                {
                    b= 0;
                    r = 255;
                }
            else
            {
                r += delta;
                b -= delta;
            }
            }
        return new Color32(r,g,b,255);
    }
}
