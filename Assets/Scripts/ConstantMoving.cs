using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMoving : MonoBehaviour {

    public Vector3 velocity = new Vector3(0f, 0f, -2f);

    private Rigidbody m_rigidBody;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.velocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position += velocity;
        m_rigidBody.velocity = velocity;
    }
}
