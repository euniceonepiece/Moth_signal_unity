using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTarget : MonoBehaviour
{
    public Transform FollowTarget;
    public float Radius;
    public float Smooth;
    public float ySmooth;
    private Vector3 smoothPosition = Vector3.zero;
    void Start()
    {
        if (!FollowTarget)
        {
            this.enabled = false;
        }
        else
        {
            transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckMargin())
        {
            smoothPosition.y = Mathf.Lerp(transform.position.y, FollowTarget.position.y+1, ySmooth * Time.deltaTime);
            smoothPosition.x = Mathf.Lerp(transform.position.x, FollowTarget.position.x, Smooth * Time.deltaTime);
            smoothPosition.z = Mathf.Lerp(transform.position.z, FollowTarget.position.z, Smooth * Time.deltaTime);
            transform.position = smoothPosition;
        }
    }
    bool CheckMargin()
    {
        return Vector3.Distance(transform.position, FollowTarget.position) > Radius;
    }
}
