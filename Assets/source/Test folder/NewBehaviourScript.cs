using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    public float speed = 20; //[1] 物體移動速度
    public Transform target;
    public Transform targetA;// [2] 目標
    public float delta = 0.8f; // 誤差值
    public float delta1 = 10f;
    private static int i = 0;
    public bool arrive;
    public Animator anim;
    public GameObject expp;
    public GameObject FireA;
    private SkinnedMeshRenderer MyRenderer;
    public GameObject Man;
    public float timeCount;

    void SkillFire()
    {
        Instantiate(FireA, targetA.transform.position, FireA.transform.rotation);
    }
    void Start()
    {
        arrive = true;
        MyRenderer = Man.GetComponent<SkinnedMeshRenderer>();
    }
    void Update()
    {
        timeCount -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack1");
            transform.LookAt(target);

        }
        anim.SetBool("attack", false);
        if (Input.GetKeyDown("1"))
        {
            arrive = false;
        }
        if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            arrive = true;
            anim.SetBool("attack", false);
            anim.SetBool("run1", false);
            MyRenderer.enabled = true;
        }
        if (arrive == false)
        {
            moveTo();
        }
    }

    void moveTo()
    {
        anim.SetBool("run1", true);
        // [3] 重新初始化目標點
        target.position = new Vector3(target.position.x, transform.position.y, target.position.z);

        // [4] 讓物體朝向目標點 
        transform.LookAt(target);
        // [5] 物體向前移動
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // [6] 判斷物體是否到達目標點
        if (transform.position.x > target.position.x - delta1
            && transform.position.x < target.position.x + delta1
            && transform.position.z > target.position.z - delta1
            && transform.position.z < target.position.z + delta1)
        {
            speed = 50;
            anim.SetBool("attack", true);
            MyRenderer.enabled = false;
            if (timeCount <= 0)
            {
                timeCount = 0.02f;
                Instantiate(expp, transform.position, expp.transform.rotation);
            }
        }

        if (transform.position.x > target.position.x - delta
       && transform.position.x < target.position.x + delta
       && transform.position.z > target.position.z - delta
       && transform.position.z < target.position.z + delta)
        {
            arrive = true;
            speed = 20;
            anim.SetBool("run1", false);
            MyRenderer.enabled = true;
        }

    }
}
