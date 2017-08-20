using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour {

    public Transform lookAtTarget;

    private Vector3 tmpPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tmpPosition = lookAtTarget.position;
        tmpPosition.y = transform.position.y;
        transform.LookAt(tmpPosition);
	}
}
