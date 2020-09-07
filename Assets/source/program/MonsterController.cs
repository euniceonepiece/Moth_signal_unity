using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
     [Header("怪物能力值")]
    public float MHP = 2000;
    public float MAttack = 15;
    //技能CD
    public float Timer1;
    public float Timer2;
    public float CD1=15;
    public float CD2=20;
    public bool dead;


    public GameObject MBLOOD;

    public GameObject MyTarget;
    private NavMeshAgent MyAgent;
    private Animator mos;
    [Header("介面")]
    public Image MBarHP;
    public GameObject Blood;
    private  float MMaxHP;
    private void Damage(float damage)
    {
        MHP-=30;
        mos.SetTrigger("damage");
        MBarHP.fillAmount = MHP / MMaxHP;
        if(MHP<=0)Dead();
    }
    private void OnTriggerEnter(Collider other)
    {
      if((other.tag == "attack" || other.tag == "bullet")&&(dead == false))
      {
          Damage(other.transform.root.GetComponent<Character>().Attack);
      }
    }
    private void Dead()
    {
        mos.SetBool("dead",true);
        MBLOOD.SetActive(false);
        Destroy(gameObject,20);
        dead = true;
    }
    void Start()
    {
        MBLOOD.SetActive(false);
        MyAgent = GetComponent<NavMeshAgent>();
        mos = GetComponent<Animator>();
        MMaxHP = MHP;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == false) { 
        float dis =  Vector3.Distance(transform.position,MyTarget.transform.position);
       if(dis>=50)
       {
        
        mos.SetBool("walkk", false );
        MBLOOD.SetActive(false);
        MyAgent.ResetPath();
       }
       else if (dis<50 && dis>=10)
       {
           mos.SetBool("walkk", true );
           mos.SetBool("attack1", false);
           mos.SetBool("attack2", false);
           MyAgent.SetDestination(MyTarget.transform.position);
           MBLOOD.SetActive(true);
           //MyAgent.stoppingDistance = 3.0f;
       }
       else if (dis<15 && dis>4)
       {
           mos.SetBool("walkk",false);
           mos.SetBool("attack2", false);
           Timer1 += Time.deltaTime;
           if(Timer1>=CD1)
           {
           Timer1 = 0;
           mos.SetBool("attack1", true);
           }
       }
       else if (dis<8)
       {
           mos.SetBool("walkk",false);
           mos.SetBool("attack1", false);
           Timer2 += Time.deltaTime;
           if(Timer2 >=CD2)
           {
           Timer2 = 0;
           mos.SetBool("attack2", true);
           }
       }

    }
    }
}
