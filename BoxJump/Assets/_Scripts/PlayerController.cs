using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float powerMultiplier;
    public float maxPower;
    public event EventHandler Landing;

    Rigidbody2D body;
    BoxCollider2D boxCollider;
    LineRenderer lineRenderer;
    bool isPressed;
    Vector2 startInputPosition;
    Vector2 endInputPosition;
    Vector2 leftDownCorner;
    Vector2 rightDownCorner;
    bool isOnGround;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGround();
        if (isPressed)
        {
            endInputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawLine();
        }

    }

    private void DrawLine()
    {
        Vector2 endLinePosition;
        
        if (Vector2.Distance(startInputPosition, endInputPosition) > maxPower)
        {
            endLinePosition = (Vector2)transform.position+(endInputPosition-startInputPosition).normalized*maxPower;
        }
        else
        {
            endLinePosition = endInputPosition;
        }
        Vector3[] positions = new Vector3[2];
        positions[0] = Vector3.zero;
        positions[1]  =-transform.InverseTransformPoint(endLinePosition);
        lineRenderer.SetPositions(positions);
    }

    private void CheckForGround()
    {
        leftDownCorner = boxCollider.bounds.min;
        rightDownCorner = leftDownCorner + Vector2.right * boxCollider.bounds.size.x;

        RaycastHit2D leftInfo = Physics2D.Raycast(leftDownCorner,Vector2.down,.5f);
        RaycastHit2D rightInfo = Physics2D.Raycast(rightDownCorner, Vector2.down, .5f);

        if(leftInfo.collider == null && rightInfo.collider == null)
        {
            isOnGround = false;
        }
        else
        {
            if(isOnGround == false)
            {
                OnLanding();
            }
            isOnGround = true;
        }
    }

    private void OnMouseDown()
    {
        lineRenderer.enabled = true;
        startInputPosition = transform.position;
        isPressed = true;
    }
    private void OnMouseUp()
    {

        lineRenderer.enabled = false;
        Vector2 direction = startInputPosition - endInputPosition;
        direction.Normalize();
        float distance = Vector2.Distance(endInputPosition, startInputPosition);
        if (distance > maxPower) distance = maxPower;
        body.AddForce(direction*distance*powerMultiplier,ForceMode2D.Impulse);
        isPressed = false;
    }

    protected virtual void OnLanding()
    {
        Landing?.Invoke(this, EventArgs.Empty);
    }
    public bool IsSafe()
    {
        return isOnGround;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftDownCorner,leftDownCorner+Vector2.down*.5f);
        Gizmos.DrawLine(rightDownCorner,rightDownCorner+Vector2.down*.5f);
    }
}
