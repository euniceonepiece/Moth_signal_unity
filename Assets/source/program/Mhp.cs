using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Mhp : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Blood;
    public GameObject target;
    public float offsetY =200;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetP = Camera.main.WorldToScreenPoint(target.transform.position);
        Blood.GetComponent<RectTransform>().position = targetP + Vector2.up*offsetY;
    }
}
