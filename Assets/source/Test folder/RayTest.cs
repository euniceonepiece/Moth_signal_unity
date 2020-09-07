using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{

    //功能 : 平面範圍偵測 概念導向

    #region Enum
    public enum Density
    {
        Low,
        Medium,
        High
    }
    #endregion

    #region Serialize Field
    [SerializeField]
    private int Angle = 20;
    [SerializeField]
    [Range(1, 100)]
    private float Range = 5;
    [SerializeField]
    [Range(-180, 180)]
    private int StartAngle = 180;
    [SerializeField]
    private Density _rayCount = Density.Low;
    #endregion

    #region Readonly
    private readonly int Count = 10;
    #endregion

    #region Unity Method
    void Update()
    {
        getRotation();
    }
    #endregion

    #region Function
    /// <summary>
    /// 計算旋轉座標
    /// </summary>
    private void getRotation()
    {
        Quaternion rot = transform.rotation;

        for (int i = 0; i < Count * (_rayCount.GetHashCode() + 1); i++)
        {
            Quaternion q = Quaternion.Euler(rot.x, rot.y + StartAngle + (Angle * i), rot.z);
            Vector3 newVec = q * transform.forward * Range;

            OnRay(newVec);


            Ray ray = new Ray(transform.position, newVec);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);

            if ((Physics.Raycast(ray, out hit, Range)) && (hit.collider.gameObject.tag == "Player"))
            {
                Debug.Log("123187");

            }
        }
    }

    /// <summary>
    /// 執行射線(Ray)判斷
    /// </summary>
    /// <param name="vec"></param>
    private void OnRay(Vector3 vec)
    {
        Debug.DrawRay(transform.position, vec, Color.red);
    }
    #endregion
}