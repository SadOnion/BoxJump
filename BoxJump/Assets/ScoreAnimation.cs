using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.instance.player.Landing += PointAnimation;
    }

    void PointAnimation(object sender,EventArgs e)
    {
        anim.SetTrigger("Point");
    }
}
