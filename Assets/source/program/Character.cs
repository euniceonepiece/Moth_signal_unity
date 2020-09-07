using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Character : MonoBehaviour
{
    public AudioSource m_ExplosionAudio;
    [Header("walk")]
    public AudioEvent m_ExplosionAudioEvent1;
    [Header("角色能力值")]
    public float HP = 200;
    public float Attack = 50;
    public float moveSpeed = 10;
    public float Timer1;
    public float CD1 = 0.3f;
    public Vector3 moveVector;
    public Transform mainCameraTran;
    public Transform CameraDir;
    private CharacterController charCtrl;
    public float rotaSpeed = 0.05f;
    private float yVelocity = 0.0F;
    private float verticalVelocity;
    public float gravity = 20.0f;
    public float jumpForce = 10.0f;
    private const int MAX_JUMP = 3;
    private int currentJump = 0;
    public float Speed;

    [Header("介面")]
    public Image BarHP;
    private float MaxHP;
    public Animator anim;

    private void ClimbStop()
    {
        if (anim.GetCurrentAnimatorStateInfo(3).IsName("Climb Layer.Climb"))
        {
            anim.enabled = false;
            Speed = 0;
            if (Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
            {
                anim.enabled = true;
                Speed = 1;
            }
        }
    }
    private void AttackSpeed()
    {
        if (anim.GetCurrentAnimatorStateInfo(2).IsName("Attack Layer.attack1"))
        {
            moveVector = CameraDir.forward * moveSpeed;
        }
    }
    private void walk()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(2);
        if (info.normalizedTime >= 1.0f)
        {
            if (charCtrl.isGrounded)
            {
                m_ExplosionAudioEvent1.Play(m_ExplosionAudio);
            }
        }
    }
    private void Damage()
    {
        HP-=30;
        anim.SetTrigger("damage");
        BarHP.fillAmount = HP / MaxHP;
    }

    private void Attacklayer()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(3);
        if (info.normalizedTime >= 1.0f)
        {
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("Attack2");
            }
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack1");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
      if(other.tag =="ATTACKmos")
      {
          Damage();
        
      }
      if(other.tag =="hpup")
      {
          if(HP>=MaxHP)
          {
          HP=200;
          BarHP.fillAmount = HP / MaxHP;
          }
          else if(HP<MaxHP)
          {
          HP+=10;
          BarHP.fillAmount = HP / MaxHP;
          }
      }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Climb")
        {
            ClimbStop();
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("Climb", true);
                Speed = 1;
                anim.enabled = true;
                verticalVelocity = 2;
                currentJump = 0;
            }
            else {
                anim.SetBool("Climb", false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Climb")
        {
            Speed = 1;
            anim.enabled = true;
            anim.SetBool("Climb", false);
        }
    }

    public void SmoothRotationY(float iTargetAngle)
    {
        transform.eulerAngles=new Vector3(0,Mathf.SmoothDampAngle(transform.eulerAngles.y,iTargetAngle,ref yVelocity,rotaSpeed),0);
    }
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        mainCameraTran = Camera.main.gameObject.transform;
        GameObject CameraDir_obj = new GameObject();
        CameraDir_obj.transform.parent = transform;
        CameraDir_obj.transform.localPosition = Vector3.zero;
        CameraDir_obj.name ="CameraDir";
        CameraDir = CameraDir_obj.transform;
        verticalVelocity = -gravity * Time.deltaTime;
        currentJump = 0;
        MaxHP = HP; 

        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Attacklayer();
        AttackSpeed();
        if (mainCameraTran)
        {
            CameraDir.eulerAngles = new Vector3(0,mainCameraTran.eulerAngles.y,0);
        }
        if(charCtrl.isGrounded)
         {
            verticalVelocity = -gravity * Time.deltaTime;
            currentJump = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(MAX_JUMP > currentJump) { 
            anim.SetBool("JUMP", true);
            verticalVelocity = jumpForce;
            currentJump++;
            }
        }
        else
           {
            anim.SetBool("JUMP", false );
            verticalVelocity-= gravity * Time.deltaTime;
        }
           anim.SetBool("RUN", false );
           anim.SetBool("ATTACK", false );
           anim.SetBool("ATTACK 0", false );
           anim.SetBool("ROLL", false);
         if(Input.GetKeyDown(KeyCode.LeftShift))
        {
              anim.SetBool("ROLL", true);
        }
        if(Input.GetKey(KeyCode.W))
        {
            anim.SetBool("RUN", true);
            SmoothRotationY(CameraDir.eulerAngles.y);
            moveVector = CameraDir.forward * moveSpeed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            anim.SetBool("RUN", true);
            SmoothRotationY(CameraDir.eulerAngles.y+ 180);
            moveVector = CameraDir.forward * -moveSpeed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
             anim.SetBool("RUN", true);
            SmoothRotationY(CameraDir.eulerAngles.y+ 90);
            moveVector = CameraDir.right * moveSpeed;
        }
        else if(Input.GetKey(KeyCode.A))
        {
             anim.SetBool("RUN", true);
            SmoothRotationY(CameraDir.eulerAngles.y+ -90);
            moveVector = CameraDir.right * -moveSpeed;
        }
         else
        {
            moveVector = Vector3.zero;
        }
        moveVector.y  = verticalVelocity;
        charCtrl.Move(moveVector* Speed * Time.deltaTime);
    }
}

