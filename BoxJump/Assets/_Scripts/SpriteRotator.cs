using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    public float rotationSpeed;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsSafe())
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            Debug.Log("r");
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0,0,rotationSpeed*Time.deltaTime));
    }
}
