using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uI;

    public int score { get; private set; }
    // Start is called before the first frame update
    public PlayerController player;
    private Rigidbody2D playerBody;
    private const float checkForGameOverTime = .8f;
    private float timer;
    private void Awake()
    {
        instance = this;
        timer = checkForGameOverTime;
        player = FindObjectOfType<PlayerController>();
        playerBody = player.gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerBody.velocity.y < 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                Collider2D[] coliders  = Physics2D.OverlapBoxAll(player.transform.position + Vector3.down * 13+Vector3.right*12, new Vector2(25, 25), 0);
                if (coliders.Length == 0) GameOver();
                if(coliders.Length == 1)
                {
                    if (coliders[0] == player.GetComponent<Collider2D>()) GameOver();
                }
                timer = checkForGameOverTime;
            }
        }
        else
        {
            timer = checkForGameOverTime;
        }
        
        
    }

    public void AddPoint()
    {
        score++;
        uI.UpdateUI();
        uI.ScoreAnimation();
    }
    public void AddPoint(int amount)
    {
        score+=amount;
        uI.UpdateUI();
        uI.ScoreAnimation();
    }

    public void GameOver()
    {
        player.Die();
        uI.ShowGameOverPanel();
    }
    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    
}
