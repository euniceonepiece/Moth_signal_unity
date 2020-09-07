using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abc : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
      if(other.name == "Cube")
      {
        gameObject.GetComponent<ParticleSystem>().Play();

      }
    }
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
    }
}