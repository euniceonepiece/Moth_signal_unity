using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public Vector3 linkStart;
    public Vector3 linkEnd;
    private NavMeshAgent Navi;
    public bool is_jumping;
    public bool Climb;
    public bool atWall;
    public GameObject player, Target;

    public Animator anim;
    public float lookRadio = 30;
    public bool isAttack;
    public bool isAttack2=false;
    public float distance;

    public float[] CDs = new float[4];
    public float[] CDTimes = new float[4];
    public float delta = 0.5f;

    public float smoothTime = 0.5F;
    private Vector3 velocity = Vector3.zero;

    public GameObject bullet;
    public GameObject Gun1,Gun2;
    public int gun;

    void Start()
    {
        Navi = GetComponent<NavMeshAgent>();   
        is_jumping = false;
        anim = GetComponent<Animator>();
        isAttack = false;
        Navi.autoTraverseOffMeshLink = false;
        Target = player;
        atWall = false;
        gun = 0;
    }

    void link()
    {
        is_jumping = true;
        OffMeshLinkData link = Navi.currentOffMeshLinkData; 
        float distS = (transform.position - link.startPos).magnitude;
        float distE = (transform.position - link.endPos).magnitude;
        anim.SetTrigger("jump");

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
        Vector3 linkCenter = (linkStart + linkEnd) * 0.5f;
        if (transform.position.y < linkCenter.y) {
            if ((linkEnd.y - linkStart.y) > 10) {
                is_jumping = false;
                Climb = true;
                smoothTime = (linkEnd.y - linkStart.y) / 4;
                anim.SetBool("Climbing", true);
                anim.SetBool("run", false);
            }
        }
        if ((transform.position.y > linkCenter.y)&& ((linkStart.y - linkEnd.y) > 10))
        {
            smoothTime = (linkStart.y - linkEnd.y) / 30;
        }

    }

    void linkcontrol() {
        if ((is_jumping == false)&&(Climb == false))
        {
            M_Move();
        }

        if (is_jumping == true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, linkEnd, ref velocity, smoothTime);
            Vector3 linkEnd1 = new Vector3(linkEnd.x, transform.position.y, linkEnd.z);
            transform.LookAt(linkEnd1);

            if (transform.position.x > linkEnd.x - delta
       && transform.position.x < linkEnd.x + delta
       && transform.position.z > linkEnd.z - delta
       && transform.position.z < linkEnd.z + delta
       && transform.position.y > linkEnd.y - delta
       && transform.position.y < linkEnd.y + delta)
            {
                smoothTime = 0.5f;
                anim.SetBool("Climbing", false);
                is_jumping = false;
                Navi.enabled = true;
            }
        }
        else if (Climb == true)
        {
            Vector3 ClimblinkEnd = new Vector3(linkStart.x, linkEnd.y, linkStart.z);
            Vector3 linkEnd1 = new Vector3(linkEnd.x, transform.position.y, linkEnd.z);
            if (atWall == false)
            {
                transform.position = Vector3.SmoothDamp(transform.position, linkStart, ref velocity, 0.3f);
            }

            if ((transform.position.x > linkStart.x - 2
         && transform.position.x < linkStart.x + 2
         && transform.position.z > linkStart.z - 2
         && transform.position.z < linkStart.z + 2))
            {
                atWall = true;
            }
            if (atWall == true)
            {
                transform.position = Vector3.SmoothDamp(transform.position, ClimblinkEnd, ref velocity, smoothTime);
                transform.LookAt(linkEnd1);
            }

            if (
              transform.position.y > ClimblinkEnd.y - 3
           && transform.position.y < ClimblinkEnd.y + 3
            )
            {
                transform.position = Vector3.SmoothDamp(transform.position, linkEnd, ref velocity, 0.3f);
                anim.SetBool("Climbing", false);

                if ((transform.position.x > linkEnd.x - delta
             && transform.position.x < linkEnd.x + delta
             && transform.position.z > linkEnd.z - delta
             && transform.position.z < linkEnd.z + delta))
                {
                    is_jumping = false;
                    Climb = false;
                    Navi.enabled = true;
                    smoothTime = 0.5f;
                }
            }
        }


        if (Navi.isOnOffMeshLink)
        {

            link();
            Navi.enabled = false;
        }

    }

    void Update()
    {
        linkcontrol();
        shoot();

        CD();
    }

    void M_Move()
    {
        Navi.stoppingDistance = 1.5f;
        Navi.SetDestination(Target.transform.position);
        float distance = Vector3.Distance(Target.transform.position, transform.position);
        anim.SetBool("run", false);
        if (distance > lookRadio)
        {
            Navi.ResetPath();
        }
        if ((distance <= lookRadio) && (distance >= 3f))
        {
            anim.SetBool("run", true);
        }
        else if (((CDs[1] >= CDTimes[1])) && (Target == player) && (isAttack2 == false) && (distance <= 2))
        {
            anim.SetBool("run", false);
            anim.SetTrigger("Punch");
            CDs[1] = 0;
        }
        if (isAttack2 == false)
        {
            Target = player;
        }
        if ((distance <= 35 && distance > 15) && (CDs[3] >= CDTimes[3]))
        {
            anim.SetTrigger("shoot");
            CDs[3] = 0;
            anim.SetBool("run", false);

        }
        else if ((distance <= 15 && distance > 10) && (CDs[2] >= CDTimes[2])&&(isAttack2==false))
        {
            Invoke("ChTarget", 0.2f);
            anim.SetTrigger("attack1");
            CDs[2] = 0;
            anim.SetBool("run", false);
        }
    }


    private void OnTriggerEnter(Collider c)
    {
        /*if (c.tag == "floor")
        {
            if (timer_land <= 0)
            {
                is_jumping = false;
                timer_land = 0;
                Navi.enabled = true;
            }
        }*/


    }

    void shoot() {
        if ((gun == 1) && (CDs[0] >= CDTimes[0])) {
            Instantiate(bullet, Gun1.transform.position, Gun1.transform.rotation);
            bullet.GetComponent<Bullet1>().target = player;
            CDs[0] = 0;
        }
        else if ((gun == 2) && (CDs[0] >= CDTimes[0]))
        {
            Instantiate(bullet, Gun2.transform.position, Gun2.transform.rotation);
            bullet.GetComponent<Bullet1>().target = player;
            CDs[0] = 0;
        }
    }
    void ChTarget()
    {
        CancelInvoke("ChTarget");
        isAttack2 = true;
        GameObject AttackTarget = new GameObject();
        AttackTarget.transform.position = player.transform.position;
        Target = AttackTarget;
        Invoke("isAttack2false", 3);
        Destroy(AttackTarget, 10);
    }
    void CD() {

        if (CDs[1] < CDTimes[1])
        {
            CDs[1] += Time.deltaTime;
        }
        if (CDs[2] < CDTimes[2])
        {
            CDs[2] += Time.deltaTime;
        }
        if (CDs[3] < CDTimes[3])
        {
            CDs[3] += Time.deltaTime;
        }
        if (CDs[0] < CDTimes[0])
        {
            CDs[0] += Time.deltaTime;
        }
    }
    void cold()
    {
        anim.ResetTrigger("hurt");
    }

    void isAttack2false()
    {
        isAttack2 = false;
    }
    void isAttackfalse()
    {
        isAttack = false;
    }

}