using System;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public PlayerController player;
    [Range(0,5)]
    public float smoothSpeed;
    public readonly float offset = 7;
    private Vector3 lastPosition;

    bool changePosition;

    // Start is called before the first frame update
    void Start()
    {
        player.Landing += ChangePosition;
        lastPosition = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (changePosition)
        {
            SmoothFollow();
        }
    }

    private void SmoothFollow()
    {
        Vector2 desiredPosition = player.transform.position + Vector3.right * offset ;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = (Vector3)smoothedPosition + Vector3.forward * -10;
        if(Vector2.Distance(transform.position,desiredPosition) < 1)
        {
            changePosition = false;
        }
    }

    private void ChangePosition(object sender,EventArgs e)
    {
        changePosition = true;
    }
}
