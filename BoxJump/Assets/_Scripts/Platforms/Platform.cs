using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool playerVisited;
    protected BoxCollider2D box;
    private Vector2 ofset;
    protected virtual void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
    protected  virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

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
        player.MakeSlime().transform.SetParent(gameObject.transform);
    }
    
}
