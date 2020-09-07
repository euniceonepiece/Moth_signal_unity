using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_attack : MonoBehaviour
{
    Animator anim;
    public Transform forward;
    public Transform player;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKey(KeyCode.Mouse1))
            {
            anim.SetBool("gun_idle", true);
            transform.rotation = Quaternion.Slerp(player.rotation, forward.rotation, speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetBool("gun_shoot", true);
            }
            else
            {
                anim.SetBool("gun_shoot", false);
            }
        }
        else
        {
            anim.SetBool("gun_idle", false);
        }
    }
}
