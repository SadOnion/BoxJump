using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Platform
{
    [SerializeField]
    Transform rotationCenter;
    [SerializeField]
    float rotationRadius, angularSpeed;

    float posX, posY, angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotationRadius = Random.Range(1f, 3f);
        angularSpeed = Random.Range(1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX,posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360) angle = 0;
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
