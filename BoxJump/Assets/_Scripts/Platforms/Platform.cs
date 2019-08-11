using System;
using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool playerVisited;
    protected BoxCollider2D box;
    private Vector2 ofset;
    private PlayerController player;
    protected virtual void Start()
    {
        box = GetComponent<BoxCollider2D>();
        player = GameManager.instance.player;
    }
    protected  virtual void OnCollisionEnter2D(Collision2D collision)
    {
        

        AddSlime(player);
        if (playerVisited == false)
        {
            StartCoroutine(CheckIfHitSide(player));
            GameManager.instance.AddPoint();
            playerVisited = true;
        }
            
           
        
        
       
    }
    IEnumerator CheckIfHitSide(PlayerController player)
    {
        yield return new WaitForEndOfFrame();
        if (player.IsSafe() == false) GameManager.instance.GameOver();
    } 
    protected virtual void AddSlime(PlayerController player)
    {
        PlayRandomSplashSound();
        player.MakeSlime().transform.SetParent(gameObject.transform);
    }

    private void PlayRandomSplashSound()
    {
        int r = UnityEngine.Random.Range(0, 2);
        if(r == 0)
        {
            AudioManager.instance.Play("Splash1");
        }
        else
        {
            AudioManager.instance.Play("Splash2");

        }
    }
}
