using UnityEngine;

public class Platform : MonoBehaviour
{
    bool playerVisited;
    protected  virtual void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (playerVisited == false && player.IsSafe())
        {
            GameManager.instance.AddPoint();
            playerVisited = true;
        }
       
    }
}
