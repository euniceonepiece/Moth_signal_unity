using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class smallM : MonoBehaviour
{
    private Vector3 linkStart;//OffMeshLink的开始点  
    private Vector3 linkEnd;//OffMeshLink的结束点  

    public Animator anim;
    public Image blood;
    public Transform player;
    public NavMeshAgent Navi;
    public float lookRadio = 30;
    public bool isDead;
    public bool isAttack;
    static float t = 0.0f;

    public float ColdTime_LightHurt = 10;
    public float timer_LightHurt;
    public float ColdTime_attack = 10;
    public float timer_attack;
    void Start()
    {

        Navi = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        timer_LightHurt = ColdTime_LightHurt;
        timer_attack = ColdTime_attack;
        isAttack = false;
        Navi.autoTraverseOffMeshLink = false;
    }


    void Update()
    {
        if ((Navi.isOnOffMeshLink)) {

            Debug.Log("2");
            CA();
        }

            //leftFootLocation.transform.eulerAngles = new Vector3(-90f, leftFootLocation.transform.rotation.y, leftFootLocation.transform.rotation.z);
        timer_LightHurt += Time.deltaTime;
        timer_attack += Time.deltaTime;
        if ((isDead == false)&&!(Navi.isOnOffMeshLink))
        {
            M_Move();
        }
        if (blood.fillAmount <= 0.001f)
        {
            anim.SetBool("died", true);
            anim.SetBool("run", false);
            isDead = true;
            Destroy(gameObject, 10);
        }
    }


    void CA() {
        OffMeshLinkData link = Navi.currentOffMeshLinkData;
        //计算角色当前是在link的开始点还是结束点（因为OffMeshLink是双向的）  
        float distS = (transform.position - link.startPos).magnitude;
        float distE = (transform.position - link.endPos).magnitude;

        if (distS < distE)
        {
            linkStart = link.startPos;
            linkEnd = link.endPos;
        }
        else
        {
            linkStart = link.endPos;
            linkEnd = link.startPos;
        }
    }

    void M_Move()
    {
        /*OffMeshLinkData link = Navi.currentOffMeshLinkData;
        if (Navi.currentOffMeshLinkData.offMeshLink != null)
        {

            //播放 爬梯子的 动画
            anim.SetBool("jump", true);
            Debug.Log("跳了");
            // Navi.speed = (Navi.speed * 0.02f);//需要改变NavMeshAgent的速度
        }*/

            Navi.ResetPath();
        Navi.stoppingDistance = 2.0f;
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= lookRadio)
        {
            anim.SetBool("run", true);
            Navi.SetDestination(player.position);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (distance <= Navi.stoppingDistance)
        {
            Navi.ResetPath();
            if (isDead == false)
            {
                if (timer_attack >= ColdTime_attack)
                {
                    anim.SetTrigger("attack");
                    timer_attack = 0;
                    isAttack = true;
                }
                anim.SetBool("run", false);
                FaceTarget();
            }
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookR = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookR, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (isDead == false)
        {
            if (c.gameObject.tag == "swordAtt")
            {
                    SwordHurt();
            }
            if (c.gameObject.tag == "bullet")
            {
                    GunHurt();
            }
    }

    }

    void SwordHurt()
    {
        blood.fillAmount -= 0.03f;
        anim.SetTrigger("hurt");
        anim.SetBool("run", false);
        Invoke("cold", 0.3f);
        Invoke("close", 2f);
        isAttackfalse();
    }

    void GunHurt()
    {

        blood.fillAmount -= 0.02f;
        anim.SetTrigger("gunHurt");
        anim.SetBool("run", false);
        Invoke("cold", 0.3f);
        Invoke("close", 1.2f);
        isAttackfalse();
    }


    void cold()
    {
        anim.ResetTrigger("hurt");
    }
    void died()
    {
        Destroy(gameObject, 7);
    }
    void isAttackfalse()
    {
        isAttack = false;
    }
}
