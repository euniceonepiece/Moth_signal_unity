using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Animator anim;
    public int Z;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Z =0;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Gun Layer.Gun Idle"))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        }
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Gun Layer.GunRun"))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        }
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Gun Layer.shoot"))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        }
        if (anim.GetCurrentAnimatorStateInfo(3).IsName("Climb Layer.Climb"))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Gun Layer"), 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("GunIdle", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetBool("GunIdle", false);
            anim.SetBool("GunRun", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("GunRun", true);
        }
        else
        {
            anim.SetBool("GunRun", false);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("shoot", true);
        }
        else
        {
            anim.SetBool("shoot", false);
        }
    }
}
