using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
      if(other.name =="Cat_Warrior")
      {
       Debug.Log("我補過血了");
       Destroy(gameObject);
      }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
