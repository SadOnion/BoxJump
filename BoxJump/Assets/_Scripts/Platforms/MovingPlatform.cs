using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    Axis axis;
    [Range(.3f,5)]
    public float maxRange;
    [Range(.3f,10)]
    public float maxSpeed;
    float range;
    float moveSpeed;
    Vector3 leftPoint;
    Vector3 rightPoint;
    Vector2 upPoint;
    Vector2 downPoint;
    Rigidbody2D body;
    bool moveRight;
    bool moveUp;
    // Start is called before the first frame update
    void Start()
    {
        range = Random.Range(.3f, maxRange);
        moveSpeed = Random.Range(.3f, maxSpeed);
        axis = (Axis)Random.Range(0, 2);
        if (axis == Axis.Horizontal)
        {
            leftPoint = transform.position + Vector3.left * range;
            rightPoint = transform.position + Vector3.right * range;
        }
        else
        {
            upPoint = transform.position + Vector3.up * range;
            downPoint = transform.position + Vector3.down * range;
        }
        
        
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > rightPoint.x)
        {
            moveRight = false;
        }
        if(transform.position.x < leftPoint.x)
        {
            moveRight = true;
        }
        if(transform.position.y > upPoint.y)
        {
            moveUp = false;
        }
        if(transform.position.y < downPoint.y)
        {
            moveUp = true;
        }
        if(axis == Axis.Horizontal)
        {
            if (moveRight)
            {
                body.MovePosition(new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y));
            }
            else
            {
                body.MovePosition(new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y));
            }
        }
        else
        {
            if (moveUp)
            {
                body.MovePosition(new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime));
            }
            else
            {
                body.MovePosition(new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime));
            }
        }
        
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(axis == Axis.Horizontal)
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * range);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * range);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * range);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * range);
        }
        
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        collision.collider.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);

    }

}
public enum Axis
{
    Vertical,
    Horizontal
}

