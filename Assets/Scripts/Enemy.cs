using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Animator m_Animator;

    Collider attackPrepareZone;
    Collider attackZone;


    GameObject attackTarget;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();

        attackPrepareZone = GameObject.Find("AttackPrepareZone").GetComponent<Collider>();
        attackZone = GameObject.Find("AttackZone").GetComponent<Collider>();

        attackTarget = Camera.main.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        
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
