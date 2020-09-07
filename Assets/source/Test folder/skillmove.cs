using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillmove : MonoBehaviour
{

    // Use this for initialization
    public float speed = 20; //[1] 物體移動速度
    public Transform target;  // [2] 目標
    public float delta = 0.3f; // 誤差值
    private static int i = 0;
    public bool arrive;
    public GameObject expp;
    public float timeCount;
    //public float timeCD;

    void Start()
    {
        arrive = false;
    }
    void Update()
    {

        transform.localEulerAngles = new Vector3(0, 90, 0);
        timeCount -= Time.deltaTime;
        if (arrive == false)
        {
            moveTo();
            if (timeCount <= 0)
            {
                timeCount = 0.1f;
                Instantiate(expp, transform.position, expp.transform.rotation);
            }
        }
    }
    void moveTo()
    {
        // [3] 重新初始化目標點
        //target.position = new Vector3(target.position.x, target.transform.position.y, target.position.z);

        // [4] 讓物體朝向目標點 
        transform.LookAt(target);
        // [5] 物體向前移動
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // [6] 判斷物體是否到達目標點
        if (transform.position.x > target.position.x - delta
       && transform.position.x < target.position.x + delta
       && transform.position.z > target.position.z - delta
       && transform.position.z < target.position.z + delta)
        {
            arrive = true;
            Destroy(gameObject);
        }

    }
}
