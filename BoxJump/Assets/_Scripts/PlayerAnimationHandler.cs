using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    Animator anim;
    PlayerController player;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player.IsSafe() == false)
        {
            anim.SetBool("InAir", true);
        }
        else
        {
            anim.SetBool("InAir", false);

        }
    }


}
