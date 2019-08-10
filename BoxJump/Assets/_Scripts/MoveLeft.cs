using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(speed,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = GameManager.instance.player;
        if(collision.GetComponent<PlayerController>() != null)
        {
            if (player.HasShield())
            {
                player.SetShield(false);

                Destroy(gameObject);
            }
            else
            {
                GameManager.instance.GameOver();

            }
        }
        
    }
    

}
