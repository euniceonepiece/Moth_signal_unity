using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp1 : MonoBehaviour
{
    public GameObject expp; //宣告爆炸效果
                            // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bad") //如果撞到的物件標籤是Bad
        {
            Instantiate(expp, transform.position, expp.transform.rotation);
        }
    }


}