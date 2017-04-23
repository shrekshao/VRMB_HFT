using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedEnemy : MonoBehaviour {
    [SerializeField]
    float m_MoveSpeedMultiplier = 1f;

    [SerializeField]
    Transform riderPivot;

    [SerializeField]
    GameObject rider;
    
    //Collider gameExitZone;

    Rigidbody m_Rigidbody;
    Animator m_Animator;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        //m_Animator.applyRootMotion = true;

        m_Rigidbody.velocity = new Vector3(0f, 0f, 4f);


        EventDelegateManager.instance.restartLevelDelegate += OnRestartLevel;
    }

    // Update is called once per frame
    void Update () {
        //rider.transform.position = riderPivot.position;
	}

    //public void OnAnimatorMove()
    //{
    //    // we implement this function to override the default root motion.
    //    // this allows us to modify the positional speed before it's applied.
    //    if (Time.deltaTime > 0)
    //    {
    //        //Debug.Log(m_Animator.deltaPosition);
    //        Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
    //        // we preserve the existing y part of the current velocity.
    //        v.y = m_Rigidbody.velocity.y;
    //        m_Rigidbody.velocity = v;
    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other == gameExitZone)
    //    {
    //        Destroy(gameObject);
    //    }
    //}


    void OnRestartLevel()
    {
        Destroy(gameObject);
    }
}
