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
    private PlayerController player;
    private Rigidbody2D playerBody;
    private float color=.1f;
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
                Collider2D[] coliders  = Physics2D.OverlapBoxAll(player.transform.position + Vector3.down * 15, new Vector2(20, 25), 0);
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
        Camera.main.backgroundColor = new Color(1-color,color,color*color);
        color += .1f;
        if (color > 1) color = .1f;
    }
    
    public void GameOver()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(-5,-1,0) + Vector3.down * 13, new Vector2(20, 25));
    }
}
