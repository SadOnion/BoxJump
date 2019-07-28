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
    // Start is called before the first frame update
    void Start()
    {
        platforms = new Queue<GameObject>();
        body = player.gameObject.GetComponent<Rigidbody2D>();
        SpawnNewPlatform(GameManager.instance.score);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - lastPlatform.position.x) < 10) NewSpawner();
        
    }
    
    void SpawnNewPlatform(int score)
    {
        
        if (score <= 10)
        {
            lastPlatform = Instantiate(platformObjects[0], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        if(score >10 && score <= 20)
        {
            lastPlatform = Instantiate(platformObjects[1], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        if(score > 20)
        {
            lastPlatform = Instantiate(platformObjects[2], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        platforms.Enqueue(lastPlatform.gameObject);
        if (platforms.Count > 3) Destroy(platforms.Dequeue());
        if (UnityEngine.Random.Range(0, 6) == 3) SpawnCoin();
    }

    private void SpawnCoin()
    {
        Instantiate(coin,lastPlatform.position+ UnityEngine.Random.Range(-3, 3)*Vector3.right+ Vector3.up*UnityEngine.Random.Range(1, 6),Quaternion.identity);
    }
    private void NewSpawner()
    {
        int score = GameManager.instance.score;
        int chanceForRotatingPlatform = Mathf.CeilToInt(Mathf.Clamp(score*.2f,0,20));
        int chanceForMovingPlatform = Mathf.CeilToInt(Mathf.Clamp(score*.4f,0,40));
        int randomValue = UnityEngine.Random.Range(0, 100);
        if(randomValue > 0 && randomValue <= chanceForRotatingPlatform)
        {
            lastPlatform = Instantiate(platformObjects[2], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        else if(randomValue > chanceForRotatingPlatform && randomValue <= chanceForMovingPlatform + chanceForMovingPlatform)
        {
            lastPlatform = Instantiate(platformObjects[1], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        else
        {
            lastPlatform = Instantiate(platformObjects[0], lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7, 12) + Vector3.up * UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
        }
        platforms.Enqueue(lastPlatform.gameObject);
        if (platforms.Count > 5) Destroy(platforms.Dequeue());
        if (UnityEngine.Random.Range(0, 6) == 3) SpawnCoin();
    }
}
