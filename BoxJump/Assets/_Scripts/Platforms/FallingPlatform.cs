using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : Platform
{
    Rigidbody2D body;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody2D>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        StartCoroutine(DisablePlatform());
    }
    IEnumerator DisablePlatform()
    {
        yield return new WaitForSeconds(.5f);
        body.bodyType = RigidbodyType2D.Dynamic;
        body.gravityScale = 2;
    }
}
