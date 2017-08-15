using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedMovement : MonoBehaviour {

    [SerializeField]
    Vector3 movement;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = movement;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
