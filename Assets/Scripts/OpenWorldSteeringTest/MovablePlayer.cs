using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlayer : MonoBehaviour {

    private Rigidbody m_rigidBody;


    public float velocityMagnitude = 0.1f;
    //private Vector3 velocityDirection;

    // Use this for initialization
    void Start () {

        m_rigidBody = GetComponent<Rigidbody>();

        //velocityDirection = -transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = transform.position + velocityDirection * velocityMagnitude;
        transform.position = transform.position + (-transform.forward) * velocityMagnitude;

        //m_rigidBody.velocity = -transform.forward * velocityMagnitude;
        
    }

    public void Steer(float deltaAngle)
    {
        //velocityDirection = Quaternion.AngleAxis(deltaAngle, Vector3.up) * velocityDirection;
        transform.Rotate(transform.up, deltaAngle);
    }
}
