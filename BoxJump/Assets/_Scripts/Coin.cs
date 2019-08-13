using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int score;
    private PlayerController player;
    private void Start()
    {
        player = GameManager.instance.player;
        player.Landing += Evaporate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.attachedRigidbody.isKinematic)
        {
            AudioManager.instance.Play("Coin");
            GameManager.instance.AddPoint(score);
            player.Landing -= Evaporate;
            Destroy(gameObject);

        }
        
    }
    private void Evaporate(object sender,EventArgs e)
    {
        if(player.transform.position.x > transform.position.x && Mathf.Abs(player.transform.position.x-transform.position.x) > 30)
        {
            player.Landing -= Evaporate;
            Destroy(gameObject);
        }
    }
}
