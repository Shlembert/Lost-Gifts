using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPuck : MonoBehaviour
{

    public Animator anim;
    public PlayrMovement PM;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.boxUp)
        {
            anim.SetBool("puckUp", true);
        }
        else
        {
            anim.SetBool("puckUp", false);
        }
    }
}
