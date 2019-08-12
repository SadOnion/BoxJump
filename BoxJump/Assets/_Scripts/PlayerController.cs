using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public Sprite spriteInAir;
    public SpriteRenderer bubble;
    public float powerMultiplier;
    public float maxPower;
    public event EventHandler Landing;
    public GameObject slimeParticle;
    public GameObject stain;
    public float lineScaler;
    Rigidbody2D body;
    BoxCollider2D boxCollider;
    LineRenderer lineRenderer;
    TrailRenderer trailRenderer;
    bool isPressed;
    Sprite defaultSprite;
    Vector2 startInputPosition;
    Vector2 endInputPosition;
    Vector2 leftDownCorner;
    Vector2 rightDownCorner;
    bool isOnGround;
    bool doubleJump;
    bool shield;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        spriteRenderer= GetComponentInChildren<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }

        CheckForGround();
        if (isPressed)
        {
            endInputPosition = Input.mousePosition;
            DrawLine();
        }

    }

    private void DrawLine()
    {
        Vector2 endLinePosition;
        Vector2 direction = endInputPosition - startInputPosition;
        direction.Normalize();
       
        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(startInputPosition), Camera.main.ScreenToWorldPoint(endInputPosition)) > maxPower)
        {
            endLinePosition = (Vector2)transform.position+direction*maxPower*lineScaler;
        }
        else
        {
            endLinePosition = (Vector2)transform.position + direction * Vector2.Distance(Camera.main.ScreenToWorldPoint(startInputPosition), Camera.main.ScreenToWorldPoint(endInputPosition))*lineScaler ;
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
            spriteRenderer.sprite = spriteInAir;
            isOnGround = false;
        }
        else
        {
            spriteRenderer.sprite = defaultSprite;
            if(isOnGround == false)
            {
                OnLanding();
            }
            doubleJump = true;
            isOnGround = true;
        }
    }

    private void MouseDown()
    {
        if(isOnGround || doubleJump)
        {
            lineRenderer.enabled = true;
            startInputPosition = Input.mousePosition;
            isPressed = true;
            if (isOnGround == false) doubleJump = false;
        }
        
    }
    private void MouseUp()
    {
        if (isPressed)
        {
            lineRenderer.enabled = false;
            Vector2 direction = startInputPosition - endInputPosition;
            direction.Normalize();
            float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(startInputPosition), Camera.main.ScreenToWorldPoint(endInputPosition));
            if (distance > maxPower) distance = maxPower;
            body.AddForce(direction * distance * powerMultiplier, ForceMode2D.Impulse);
            isPressed = false;
        }
        
    }

    protected virtual void OnLanding()
    {
        Landing?.Invoke(this, EventArgs.Empty);
    }
    public bool IsSafe()
    {
        return isOnGround;
    }
    public void Die()
    {
        MakeSlime();
        gameObject.SetActive(false);
        
    }
    public bool HasShield()
    {
        return shield;
    }
    public void SetShield(bool b)
    {
        if (b) AudioManager.instance.Play("ShieldPick");
        else AudioManager.instance.Play("ShieldPop");
        bubble.enabled = b;
        shield = b;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftDownCorner,leftDownCorner+Vector2.down*.2f);
        Gizmos.DrawLine(rightDownCorner,rightDownCorner+Vector2.down*.2f);
    }
    public BoxCollider2D GetCollider()
    {
        return boxCollider;
    }
    public GameObject MakeSlime()
    {
        Instantiate(slimeParticle, transform.position, Quaternion.identity);
        GameObject o = Instantiate(stain, transform.position, Quaternion.Euler(0,0,UnityEngine.Random.Range(0,360)));
        o.transform.localScale *= UnityEngine.Random.Range(1.25f, 2f);
        return o;
    }
}
