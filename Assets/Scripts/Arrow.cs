using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody m_rigidBody;

    private Transform anchor = null;

    private bool flying;

	// Use this for initialization
	void Start () {
        flying = true;

        m_rigidBody = GetComponent<Rigidbody>();
        //m_rigidBody = transform.FindChild("Arrow").gameObject.GetComponent<Rigidbody>();

        m_rigidBody.velocity = new Vector3(0f, 0f, 10f);
    }

    public void InitVelocity(Vector3 v)  // TODO: rotation
    {
        //transform.position = p;
        GetComponent<Rigidbody>().velocity = v;
    }
	
	// Update is called once per frame
	void Update () {

        if (flying)
        {
            transform.LookAt(transform.position + m_rigidBody.velocity);
            transform.Rotate(Vector3.up, 90);
        } else if (anchor != null)
        {
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }
        
	}

    void OnCollisionEnter(Collision c)
    {
        if (flying)
        {
            //Debug.Log("collision");
            if (c.collider.CompareTag("Ground"))
            {
                flying = false;
                //m_rigidBody.useGravity = false;
                //m_rigidBody.velocity = Vector3.zero;
                //m_rigidBody.angularVelocity = Vector3.zero;
                transform.position = c.contacts[0].point;

                GameObject anchor = new GameObject("ANCHOR");
                anchor.transform.position = transform.position;
                anchor.transform.rotation = transform.rotation;
                anchor.transform.SetParent(c.transform);
                this.anchor = anchor.transform;

                m_rigidBody.freezeRotation = true;
                Destroy(m_rigidBody);

                GetComponent<Collider>().isTrigger = true;

                // attach to ground
                //transform.SetParent(c.collider.transform);
                //Destroy(gameObject);
            }
        }
        
    }
}
