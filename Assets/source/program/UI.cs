using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject BACK;
    // Start is called before the first frame update
    void Start()
    {
        BACK.SetActive(false);
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            BACK.SetActive(true);
            Cursor.visible = true;
        }
        else if (Input.GetKey(KeyCode.Q))
            { 
            BACK.SetActive(false);
            Cursor.visible = false;
        }
    }
}
