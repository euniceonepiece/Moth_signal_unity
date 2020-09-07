using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    public void OnAttackEnter()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1.0f);
    }
    public void AttackIdleEnter()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
    public void ClimbEnter()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Climb Layer"), 1.0f);
    }
    public void ClimbIdleEnter()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Climb Layer"), 0);
    }
    public void GunIdleEnter()
    {

      anim.SetLayerWeight(anim.GetLayerIndex("Gun Layer"), 0);

    }
    public void GunEnter()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Gun Layer"), 1);
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
