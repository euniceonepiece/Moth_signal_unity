using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject target;
    public GameObject Bullet;
    public GameObject Shooteffects;
    void shoot()
    {
        Instantiate(Bullet, target.transform.position, target.transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
        }
    }
}
