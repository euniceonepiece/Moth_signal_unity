using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;



namespace DiasGames.ThirdPersonSystem
{
    public class Guard : MonoBehaviour
    {
        private NavMeshAgent Navi;
        public GameObject Target, PatrolTarget, Player1, Player2, LeftHand, RightHand, BodyIK, UI1, PlayerLeftHand, PlayerRightHand, AttractTarget;

        public Animator anim;
        public float lookRadio = 30;
        public bool isAttack;
        public bool isAttack2;
        public bool Find;
        public bool Dead;
        public bool TryChokehold;
        public bool isChokehold;
        public bool isAttract;
        public float distance;
        public GameObject[] PatrolPosition = new GameObject[3];
        public float[] CDs = new float[3];
        public float[] CDTimes = new float[3];
        public float delta = 0.5f;
        public int PatrolPosition1 = 0;
        public int IKWeights = 0;
        public float HP;
        public float MaxHP = 1200;
        public Slider HPBar;

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
        private readonly int Count = 15;
        #endregion

        #region Unity Method

        void Start()
        {
            Find = false;
            Navi = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            isAttack = false;
            isAttack2 = false;
            Dead = false;
            isChokehold = false;
            TryChokehold = false;
            isAttract = false;
            Target = Player1;
            anim.SetLayerWeight(anim.GetLayerIndex("Chokehold Layer"), 0f);
            HP = MaxHP;
        }

        void Update()
        { 
            if ((Find == true) && (Dead == false))
            {
                M_Move();
                UI1.SetActive(false);
            }
            if ((Find == false) && (Dead == false))
            {
                if (isAttract == false) {
                    patrol();
                }
                Chokehold();
            }
            if ((Find == false) && (Dead == false) && (isAttract == true))
            {
                anim.SetBool("Walk", false);
                Attract();
            }
            else {
                anim.SetBool("Attract", false);
            }
                if ((HP < 0)&&(Dead == false))
            {
                Navi.ResetPath();
                Dead = true;
                isChokehold = false;
                Player1.GetComponent<ThirdPersonSystem>().Chokehold = false;
                IKWeights = 0;
                anim.SetLayerWeight(anim.GetLayerIndex("Chokehold Layer"), 0);
                anim.SetBool("Dead", true);
                //Destroy(HPBar.gameObject, 2);
            }
            else {
                HPBar.value = HP / MaxHP;
            }
            getRotation();
            CD();
        }

        void patrol()
        {
            anim.SetBool("Walk", true);
            PatrolTarget = PatrolPosition[PatrolPosition1];
            float distance = Vector3.Distance(PatrolTarget.transform.position, transform.position);
            Navi.SetDestination(PatrolTarget.transform.position);
            Navi.stoppingDistance = 1;
            if (distance < 1.5f)
            {
                PatrolPosition1 = PatrolPosition1 + 1;
                if (PatrolPosition1 == PatrolPosition.Length)
                {
                    PatrolPosition1 = 0;
                }
            }
        }
        void Attract()
        {
            float distance = Vector3.Distance(AttractTarget.transform.position, transform.position);
            Navi.SetDestination(AttractTarget.transform.position);
            Navi.stoppingDistance = 1;
            anim.SetBool("Attract", true);
            if (distance <= 2) {
                anim.SetTrigger("Arrival");
                Invoke("isAttractfalse", 3);
            }
        }

        void Chokehold()
        {
            float distance = Vector3.Distance(Player1.transform.position, transform.position);
            if ((distance <= 5)&&(TryChokehold == false))
            {
                UI1.SetActive(true);
            }
            else
            {
                UI1.SetActive(false);
            }
             if ((distance < 1)&& (TryChokehold == true))
            {
                UI1.SetActive(false);
                if (CDs[0] >= CDTimes[0]) {
                    HP = HP-100;
                    CDs[0] = 0;
                }
                isChokehold = true;
                transform.rotation = Player1.transform.rotation;
                transform.position = Player2.transform.position;
                IKWeights = 1;
                anim.SetLayerWeight(anim.GetLayerIndex("Chokehold Layer"), 1.0f);
                Player1.GetComponent<ThirdPersonSystem>().Chokehold = true;
            }
            else
            {
                TryChokehold = false;
            }
        }


        void OnAnimatorIK()
        {
            anim.SetLookAtPosition(BodyIK.transform.position + BodyIK.transform.forward * 50f);
            anim.SetLookAtWeight(IKWeights, 1, 0f, 0f);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKWeights);

            anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHand.transform.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, IKWeights);

            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHand.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, IKWeights);

            anim.SetIKRotation(AvatarIKGoal.RightHand, RightHand.transform.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, IKWeights);
        }
        void M_Move()
        {
            Navi.stoppingDistance = 2;
            float distance = Vector3.Distance(Target.transform.position, transform.position);
            Navi.SetDestination(Target.transform.position);
            anim.SetBool("run", false);
            anim.SetBool("Walk", false);
            if (distance > lookRadio)
            {
                Find = false;
                Navi.ResetPath();
            }
            if ((distance <= lookRadio) && (distance >= 3f))
            {
                anim.SetBool("run", true);
            }
            else if (((CDs[1] >= CDTimes[1])) && (Target == Player1) && (isAttack2 == false) && (distance <= 4))
            {
                anim.SetBool("run", false);
                anim.SetTrigger("Punch");
                CDs[1] = 0;
            }
            if (isAttack2 == false)
            {
                Target = Player1;
            }
            else if ((distance <= 15 && distance > 10) && (CDs[2] >= CDTimes[2]) && (isAttack2 == false))
            {
                Invoke("ChTarget", 0.2f);
                anim.SetTrigger("attack1");
                CDs[2] = 0;
                anim.SetBool("run", false);
            }
        }


        public void OnTriggerEnter(Collider c)
        {
            if (c.tag == "Attract")
            {
                AttractTarget = c.gameObject;
                isAttract = true;
                Debug.Log("4353");
            }
        }

        void ChTarget()
        {
            CancelInvoke("ChTarget");
            isAttack2 = true;
            GameObject AttackTarget = new GameObject();
            AttackTarget.transform.position = Player1.transform.position;
            Target = AttackTarget;
            Invoke("isAttack2false", 3);
            Destroy(AttackTarget, 10);
        }
        void CD()
        {

            if (CDs[1] < CDTimes[1])
            {
                CDs[1] += Time.deltaTime;
            }
            if (CDs[2] < CDTimes[2])
            {
                CDs[2] += Time.deltaTime;
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
        void isAttractfalse()
        {
            anim.SetBool("Attract", false);
            isAttract = false;
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



                Vector3 RayPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Ray ray = new Ray(RayPosition, newVec);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit);

                if ((Physics.Raycast(ray, out hit, Range)) && (hit.collider.gameObject.tag == "Player"))
                {
                    Find = true;
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
}