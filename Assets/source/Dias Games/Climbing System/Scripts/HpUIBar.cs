using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIBar : MonoBehaviour
{
    public Slider slider;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target2 = Camera.main.WorldToScreenPoint(target.transform.position);
        slider.GetComponent<RectTransform>().position = target2+ Vector2.up * 200;
    }
}
