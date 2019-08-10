using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform lastPlatform;
    Queue<GameObject> platforms;
    public PlayerController player;
    Rigidbody2D body;
    public GameObject[] platformObjects;
    public GameObject coin;
    public GameObject shield;
    
    // Start is called before the first frame update
    void Start()
    {
        platforms = new Queue<GameObject>();
        body = player.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - lastPlatform.position.x) < 10) SpawnPlatform();
        
    }
    
    

    private void TrySpawnCoin()
    {
        if (UnityEngine.Random.Range(0, 6) == 3)
        {
            Instantiate(coin, lastPlatform.position + UnityEngine.Random.Range(-3, 3) * Vector3.right + Vector3.up * UnityEngine.Random.Range(1, 6), Quaternion.identity);
        }
    }
    private void SpawnPlatform()
    {
        
        int randomValue = UnityEngine.Random.Range(0, 100);
        lastPlatform = InstantiateNewPlatform(randomValue);
        lastPlatform.localScale = CalculateScale(GameManager.instance.score);
        platforms.Enqueue(lastPlatform.gameObject);
        if (platforms.Count > 5) Destroy(platforms.Dequeue());
        TrySpawnCoin();
        TrySpawnShield();
    }
    private void TrySpawnShield()
    {
        if (UnityEngine.Random.Range(0, 40) == 5)
        {
            Instantiate(shield, lastPlatform.position + UnityEngine.Random.Range(-3, 3) * Vector3.right + Vector3.up * UnityEngine.Random.Range(1, 6), Quaternion.identity);
        }
    }
    private Transform InstantiateNewPlatform(int seed)
    {
        int score = GameManager.instance.score;
        int chanceForRotatingPlatform = Mathf.CeilToInt(Mathf.Clamp(score * .2f, 0, 20));
        int chanceForMovingPlatform = Mathf.CeilToInt(Mathf.Clamp(score * .4f, 0, 40));
        int chanceForFallingPlatform = Mathf.CeilToInt(Mathf.Clamp(score * .4f, 0, 40));
        Transform returnTransform;
        if (seed > 0 && seed <= chanceForRotatingPlatform)
        {
            returnTransform = Instantiate(platformObjects[2], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        else if (seed > chanceForRotatingPlatform && seed <= chanceForMovingPlatform + chanceForMovingPlatform)
        {
            returnTransform = Instantiate(platformObjects[1], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }else if(seed > chanceForMovingPlatform + chanceForMovingPlatform && seed <= chanceForMovingPlatform + chanceForMovingPlatform+chanceForFallingPlatform)
        {
            returnTransform = Instantiate(platformObjects[3], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        else
        {
            returnTransform = Instantiate(platformObjects[0], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        return returnTransform;
    }
    public float LowestPoint()
    {
        GameObject[] list = platforms.ToArray();
        float minValue = float.MaxValue;
        foreach (var item in list)
        {
            if (minValue > item.transform.position.y) minValue = item.transform.position.y;
        }
        return minValue;
    }
    private Vector3 CalculateScale(int score)
    {
        float xScale;
        float yScale;
        xScale = Mathf.Clamp(1 - score * 0.003f, 0.6f, 1);
        yScale = .75f * xScale;
        return new Vector3(xScale, yScale, 1);
    }
}
