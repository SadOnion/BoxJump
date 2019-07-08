using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform lastPlatform;
    public PlayerController player;
    Rigidbody2D body;
    public GameObject simplePlatform;

    // Start is called before the first frame update
    void Start()
    {
        body = player.gameObject.GetComponent<Rigidbody2D>();
        player.Landing += SpawnNewPlatform;
    }

    // Update is called once per frame
    void Update()
    {
        if(body.velocity.y < 0 && player.transform.position.y < lastPlatform.position.y-1)
        {
            GameManager.instance.GameOver();
        }
    }

    void SpawnNewPlatform(object sender,EventArgs e)
    {
        lastPlatform = Instantiate(simplePlatform,lastPlatform.position + Vector3.right * UnityEngine.Random.Range(7,13), Quaternion.identity).transform;
    }
}
