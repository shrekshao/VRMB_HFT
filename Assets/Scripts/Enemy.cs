using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoldierType { Swordsman = 0, Spearman, Bowman };

public class Enemy : MonoBehaviour {

    


    Animator m_Animator;
    Rigidbody m_Rigidbody;

    //Collider attackPrepareZone;
    //Collider attackZone;


    //GameObject attackTarget;

    [SerializeField]
    Transform saddle;

    [SerializeField]
    GameObject arrow;   //prefab

    [SerializeField]
    Transform arrowShootPosition;

    [SerializeField]
    GameObject weapon;

    [SerializeField]
    GameObject bow;

    [SerializeField]
    GameObject rightHandArrow;   //place holder arrow used for draw bow animation

    [SerializeField]
    SoldierType soldierType;

    [SerializeField]
    Transform lookAtBone;


    // TODO: optimized with gameobject pool
    // temparaly using 
    GameObject hitEffect;

    Transform player;

    private bool initializedSoliderType;

    private bool dead;

    // Use this for initialization
    void Start () {
        dead = false;
        initializedSoliderType = false;

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        //attackPrepareZone = GameObject.Find("AttackPrepareZone").GetComponent<Collider>();
        //attackZone = GameObject.Find("AttackZone").GetComponent<Collider>();

        hitEffect = GameObject.Find("hitEffect");

        //attackTarget = Camera.main.gameObject;
        //attackTarget = GameObject.Find("PlayerCollider");


        //weapon = transform.Find("Weapon");
        weapon.GetComponent<Collider>().enabled = false;


        player = GameObject.Find("Player").transform;

        initSoldier();
    }

    //public void setSoldierType(int type)
    //{
    //    soldierType = (SoldierType)type;
    //}

    public void SetSoldierType(SoldierType type)
    {
        soldierType = type;

        initSoldier();
    }

    void initSoldier()
    {
        if (!initializedSoliderType)
        {
            initializedSoliderType = true;
            m_Animator = GetComponent<Animator>();
            switch (soldierType)
            {
                case SoldierType.Swordsman:
                    {
                        m_Animator.SetBool("isArcher", false);
                        rightHandArrow.SetActive(false);
                        break;
                    }
                case SoldierType.Bowman:
                    {
                        m_Animator.SetBool("isArcher", true);

                        weapon.SetActive(false);
                        bow.SetActive(true);
                        rightHandArrow.SetActive(false);

                        StartCoroutine(DrawBow());
                        break;
                    }
            }
        }
        
    }

    void deconstructSoldier()
    {
        switch (soldierType)
        {
            case SoldierType.Swordsman:
                {
                    break;
                }
            case SoldierType.Bowman:
                {

                    StopCoroutine(DrawBow());
                    break;
                }
        }
    }


    private bool ikActive = true;
    void OnAnimatorIK()
    {
        if (m_Animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (player != null)
                {
                    //Vector3 targetPosition = new Vector3(player.position.x, lookAtBone.position.y, player.position.z);
                    Vector3 targetPosition = new Vector3(player.position.x, m_Animator.rootPosition.y, player.position.z);

                    m_Animator.SetLookAtWeight(1, 1, 0, 0);
                    m_Animator.SetLookAtPosition(player.position);
                }
                

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                m_Animator.SetLookAtWeight(0);
            }
        }
    }


    private bool isArrowAimingFix = false;
    void LateUpdate()
    {
        //// used for look at
        //// set bones transform after the animation
        //if (ikActive)
        //{
        //    if (player != null)
        //    {
        //        // only rotate on Y (up) axis
        //        Vector3 targetPosition = new Vector3(player.position.x, lookAtBone.position.y, player.position.z);

        //        lookAtBone.LookAt(targetPosition, Vector3.up);
        //    }
        //}


        // tiny y axis rotation fix for arrow aiming
        if (isArrowAimingFix)
        {
            Vector3 targetVector = player.position - rightHandArrow.transform.position;
            targetVector.y = 0;
            Vector3 currentVector = new Vector3(rightHandArrow.transform.forward.x, 0, rightHandArrow.transform.forward.z);

            float deltaAngle = Vector3.Angle(currentVector, targetVector);

            //Debug.Log("delta: " + deltaAngle);
            //Debug.Log("current: " + lookAtBone.rotation.eulerAngles.y);

            lookAtBone.Rotate(Vector3.up, deltaAngle, Space.World);

            //Debug.Log("after: " + lookAtBone.rotation.eulerAngles.y);
        }
    }




    // Update is called once per frame
    private bool deathForce = false;
	void Update () {
        if (!dead)
        {
            transform.position = saddle.position;
        }
        else
        {
            // dead

            if (!deathForce)
            {
                deconstructSoldier();

                // add force doesn't seem to work in OnEnterCollision
                m_Rigidbody.AddForce(40f * Vector3.back + 2f * Vector3.up, ForceMode.Impulse);
                deathForce = true;

                StartCoroutine(WaitAndDestroy());
            }
            
        }
        
	}

    IEnumerator DrawBow()
    {

        while(true)
        {
            yield return new WaitForSeconds(2);
            //Debug.Log("Shoot");
            //GameObject arrow = (GameObject) Instantiate(this.arrow, arrowShootPosition.position + 2f * Vector3.forward, Quaternion.identity);

            m_Animator.SetTrigger("ArcherDrawBow");
            m_Animator.SetTrigger("ArcherShoot");

           
        }
        
    }

    public void PickArrow()
    {
        rightHandArrow.SetActive(true);
    }


    public void SetAimArrowFix(bool v)
    {
        isArrowAimingFix = v;
    }

    public void ShootArrow()
    {
        isArrowAimingFix = false;


        //+ 0.1f * Vector3.forward
        GameObject arrow = (GameObject)Instantiate(this.arrow, rightHandArrow.transform.position + 0.2f * rightHandArrow.transform.forward, rightHandArrow.transform.rotation);

        rightHandArrow.SetActive(false);

        //arrow.GetComponent<Arrow>().InitVelocity(new Vector3(0f, 12f, 30f));

        arrow.GetComponent<Arrow>().InitVelocity(
            Vector3.up * 3f
            + rightHandArrow.transform.forward * 20f
            );
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


    void UpdateDeathState()
    {
        dead = true;
        ikActive = false;
        isArrowAimingFix = false;
        rightHandArrow.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerAttack") && !dead)
        {
            // killed  by player
            //Debug.Log(other.name);
            UpdateDeathState();



            transform.SetParent(null);
            m_Rigidbody.useGravity = true;
            //m_Rigidbody.velocity = 20f * Vector3.back;
            //m_Rigidbody.AddForce(100f * Vector3.back + 10f * Vector3.up, ForceMode.Impulse);
            //m_Rigidbody.AddForce(10f * Vector3.back, ForceMode.Force);\
            m_Animator.SetLayerWeight(1, 0);
            //m_Animator.SetTrigger("Damaged");
            m_Animator.SetTrigger("Death");


            //hitEffect.transform.position = other.gameObject.transform.FindChild("SwordMidPoint").transform.position;
            hitEffect.transform.position = collision.contacts[0].point;
            hitEffect.GetComponent<ParticleSystem>().Play();
            hitEffect.GetComponent<AudioSource>().Play();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (soldierType == SoldierType.Swordsman)
        {
            if (other.CompareTag("AttackPrepareZoneFriend"))
            {

                // attack prepare
                m_Animator.ResetTrigger("SwingAttackBothSide");
                if (0 < Vector3.Dot(transform.right, other.transform.position - transform.position))
                {
                    m_Animator.SetBool("SwingRightStart", true);
                }
                else
                {
                    m_Animator.SetBool("SwingLeftStart", true);
                }


            }
            else if (other.CompareTag("AttackExecuteZoneFriend"))
            {

                // attack execute

                m_Animator.SetBool("SwingRightStart", false);
                m_Animator.SetBool("SwingLeftStart", false);
                m_Animator.SetTrigger("SwingAttackBothSide");


                weapon.GetComponent<Collider>().enabled = true;
                StartCoroutine(setWeaponTriggerEnable(false));
            }
        }
        
    }


    IEnumerator setWeaponTriggerEnable(bool e)
    {
        yield return new WaitForSeconds(2);
        weapon.GetComponent<Collider>().enabled = e;
    }

}
