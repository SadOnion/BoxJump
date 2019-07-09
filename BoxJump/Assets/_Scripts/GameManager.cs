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
    private float color=.1f;
    private void Awake()
    {
        instance = this;
 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
}
