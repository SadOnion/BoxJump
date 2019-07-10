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
    private const int checkForGameOverTime = 1;
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
                Collider2D[] coliders  = Physics2D.OverlapBoxAll(player.transform.position + Vector3.down * 13+Vector3.right*14, new Vector2(25, 25), 0);
                if (coliders.Length == 0) GameOver();
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
    }
    
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(-5,-1,0) + Vector3.down * 12.5f+Vector3.right * 11.5f, new Vector2(25, 25));
    }
}
