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
        if (Vector2.Distance(player.transform.position, lastPlatform.position) < 10) SpawnNewPlatform(GameManager.instance.score);
        
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
       
    }
}
