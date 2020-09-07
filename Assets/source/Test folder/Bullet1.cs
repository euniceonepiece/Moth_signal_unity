using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{

    public float speed; 
    public GameObject target;  
    public float delta = 1f; 
    private static int i = 0;
    public GameObject expp;
    public bool arrive;

    void Start()
    {
        speed = 10;
    }
        void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Invoke("moveTo", 1.5f);
    }
    void moveTo()
    {
        Destroy(gameObject,1f);
        Invoke("explosion", 0.9f);
        speed = 30;
        transform.LookAt(target.transform);

        if (transform.position.x > target.transform.position.x - delta
       && transform.position.x < target.transform.position.x + delta
       && transform.position.z > target.transform.position.z - delta
       && transform.position.z < target.transform.position.z + delta)
        {
            Destroy(gameObject);
            explosion();
        }
    }
    void explosion()
    {
        Instantiate(expp, transform.position, expp.transform.rotation);
    }
}
