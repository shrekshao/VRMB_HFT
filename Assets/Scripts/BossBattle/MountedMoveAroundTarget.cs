using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedMoveAroundTarget : MonoBehaviour {

    const float maxAccMagnitude = 1.0f;
    const float maxVelMagnitude = 4.0f;

    public Rigidbody target;
    public float radiusMin = 1f;
    public float radiusMax = 7.0f;
    const float radianMin = -Mathf.PI / 2f;
    const float radianMax = Mathf.PI / 2f;


    Vector3 targetPosition;
    Vector3 targetVelocity;
    Rigidbody m_rigidBody;

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(RandomTargetPosition());
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 deltaPosition = targetPosition - transform.position;
        float vmagnitude = deltaPosition.magnitude;
        targetVelocity = Mathf.Clamp(vmagnitude, 0f, maxVelMagnitude) * deltaPosition / vmagnitude;
        targetVelocity += target.velocity;
        Vector3 deltaVelocity = targetVelocity - m_rigidBody.velocity;
        deltaVelocity.y = 0;

        Vector3 acc = deltaVelocity.normalized * maxAccMagnitude;

        m_rigidBody.velocity += acc;
        //m_rigidBody.velocity.y = 0;
	}


    IEnumerator RandomTargetPosition()
    {
        while(true)
        {
            float r = Random.Range(radianMin, radianMax);
            targetPosition = target.transform.position + new Vector3(Mathf.Cos(r), 0, Mathf.Sin(r)) * Random.Range(radiusMin, radiusMax);
            yield return new WaitForSeconds(3f);
        }
    }
}
