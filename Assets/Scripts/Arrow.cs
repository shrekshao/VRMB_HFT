using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody m_rigidBody;

    private Transform anchor = null;

    private bool flying;
    private bool falling;

    private HitEffectPoolManager hitEffectPoolManager;

	// Use this for initialization
	void Start () {
        flying = true;
        falling = false;

        m_rigidBody = GetComponent<Rigidbody>();
        //m_rigidBody = transform.FindChild("Arrow").gameObject.GetComponent<Rigidbody>();

        //hitEffectPoolManager = GameObject.Find("HitEffectPool").GetComponent<HitEffectPoolManager>();
        hitEffectPoolManager = HitEffectPoolManager.instance;
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

                StartCoroutine(WaitAndDestroy());
            }
            else if (c.collider.CompareTag("PlayerAttack") && !falling)
            {
                // get blocked
                falling = true;
                GameObject h = hitEffectPoolManager.getBlockArrowHitEfects();
                //h.SetActive(true);
                h.transform.position = c.contacts[0].point;
                h.GetComponent<ParticleSystem>().Play();
                h.GetComponent<AudioSource>().Play();

                m_rigidBody.velocity = -0.1f * m_rigidBody.velocity;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Arrow hit player");
            m_rigidBody.velocity.Set(0, 0, 0);
            flying = false;

            transform.SetParent(other.transform);

            GetComponent<AudioSource>().Play();

            Destroy(m_rigidBody);
            Destroy(GetComponent<Collider>());

            StartCoroutine(WaitAndDestroy());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(3);
        if (this.anchor != null)
        {
            Destroy(this.anchor.gameObject);
        }
        
        Destroy(gameObject);
    }
}
