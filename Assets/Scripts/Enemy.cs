using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Animator m_Animator;
    Rigidbody m_Rigidbody;

    Collider attackPrepareZone;
    Collider attackZone;


    GameObject attackTarget;

    [SerializeField]
    Transform saddle;

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    Transform arrowShootPosition;

    GameObject hitEffect;


    private bool dead;

    // Use this for initialization
    void Start () {
        dead = false;

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        attackPrepareZone = GameObject.Find("AttackPrepareZone").GetComponent<Collider>();
        attackZone = GameObject.Find("AttackZone").GetComponent<Collider>();

        hitEffect = GameObject.Find("hitEffect");

        attackTarget = Camera.main.gameObject;


        StartCoroutine(ShootArrow());
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
                m_Rigidbody.AddForce(40f * Vector3.back + 2f * Vector3.up, ForceMode.Impulse);
                deathForce = true;

                StartCoroutine(WaitAndDestroy());
            }
            
        }
        
	}

    IEnumerator ShootArrow()
    {

        while(true)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Shoot");
            GameObject arrow = (GameObject) Instantiate(this.arrow, arrowShootPosition.position + 2f * Vector3.forward, Quaternion.identity);

            arrow.GetComponent<Arrow>().InitVelocity(new Vector3(0f, 3f, 10f));
        }
        
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerAttack") && !dead)
        {
            // killed  by player
            //Debug.Log(other.name);
            dead = true;


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
        //if (other.CompareTag("PlayerAttack") && !dead)
        //{
        //    // killed  by player
        //    //Debug.Log(other.name);
        //    dead = true;
            
            
        //    transform.SetParent(null);
        //    m_Rigidbody.useGravity = true;
        //    //m_Rigidbody.velocity = 20f * Vector3.back;
        //    //m_Rigidbody.AddForce(100f * Vector3.back + 10f * Vector3.up, ForceMode.Impulse);
        //    //m_Rigidbody.AddForce(10f * Vector3.back, ForceMode.Force);\
        //    m_Animator.SetLayerWeight(1, 0);
        //    //m_Animator.SetTrigger("Damaged");
        //    m_Animator.SetTrigger("Death");


        //    hitEffect.transform.position = other.gameObject.transform.FindChild("SwordMidPoint").transform.position;
        //    hitEffect.GetComponent<ParticleSystem>().Play();
        //    hitEffect.GetComponent<AudioSource>().Play();
        //}
        if (other == attackPrepareZone)
        {
            //Debug.Log(other.name);
            if ( 0 < Vector3.Dot( transform.right, attackTarget.transform.position - transform.position ) )
            {
                m_Animator.SetBool("SwingRightStart", true);
            }
            else
            {
                m_Animator.SetBool("SwingLeftStart", true);
            }
            
        }
        else if (other == attackZone)
        {
            m_Animator.SetBool("SwingRightStart", false);
            m_Animator.SetBool("SwingLeftStart", false);
            m_Animator.SetTrigger("SwingAttackBothSide");
        }
    }


}
