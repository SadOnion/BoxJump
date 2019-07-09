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
        SpawnNewPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, lastPlatform.position) < 10) SpawnNewPlatform();
        if (Vector2.Distance(player.transform.position, lastPlatform.position) > 25) GameManager.instance.GameOver();
    }
    float LowestPoint()
    {
        GameObject[] arr = platforms.ToArray();
        float lowest = arr[0].transform.position.y;
        for (int i = 1; i < arr.Length; i++)
        {
            if (lowest > arr[i].transform.position.y) lowest = arr[i].transform.position.y;
        }
        return lowest;
    }
    void SpawnNewPlatform()
    {
        lastPlatform = Instantiate(platformObjects[UnityEngine.Random.Range(0,platformObjects.Length)],lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7,13)+Vector3.up* UnityEngine.Random.Range(-3, 3), Quaternion.identity).transform;
       
        platforms.Enqueue(lastPlatform.gameObject);
       
    }
}
