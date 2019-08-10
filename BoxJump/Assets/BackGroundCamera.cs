using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCamera : MonoBehaviour
{
    private Rigidbody2D body;
    public Vector2 range;
    public GameObject[] stars;
    private int currentStars;
    private int moves=1;
    // Start is called before the first frame update
    void Start()
    {
        body = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector2.right*Norm(body.velocity.x) * Time.deltaTime);
        if (transform.localPosition.x > moves * 40) Move();
    }

    private void Move()
    {
        if(currentStars == 1)
        {
            stars[currentStars].transform.localPosition += Vector3.right * 80;
            currentStars = 0;
        }
        else
        {
            stars[currentStars].transform.localPosition += Vector3.right * 80;
            currentStars = 1;
        }
        moves++;
    }
    public int Norm(float f)
    {
        if (f > 0) return 1;
        else if (f < 0) return -1;
        else return 0;
    }
}
