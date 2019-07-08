using UnityEngine;

public class Platform : MonoBehaviour
{
    bool playerVisited;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (playerVisited == false && player.IsSafe())
        {
            GameManager.instance.AddPoint();
            playerVisited = true;
        }
        if(player.IsSafe() == false)
        {
            GameManager.instance.GameOver();
        }
    }
}
