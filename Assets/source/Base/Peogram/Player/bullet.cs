using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject F;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "monster")
        {
            Instantiate(F,transform.position,transform.rotation);
            Destroy(gameObject,0.5F);
            Debug.Log("123");
        }
    }
    // Update is called once per frame
    void Update()
    {
            transform.Translate(0, 0, 65 * Time.deltaTime);
            Destroy(gameObject, 3);
    }
}
