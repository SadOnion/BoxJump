using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private PlayerController player;
    private void Start()
    {
        player = GameManager.instance.player;
        player.Landing += Evaporate;
    }

    private void Evaporate(object sender, EventArgs e)
    {
        if (player.transform.position.x > transform.position.x && Mathf.Abs(player.transform.position.x - transform.position.x) > 50)
        {
            player.Landing -= Evaporate;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.player.SetShield(true);
            player.Landing -= Evaporate;
            Destroy(gameObject);
        }
    }
}
