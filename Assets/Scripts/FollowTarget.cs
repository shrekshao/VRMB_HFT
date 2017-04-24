using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    [SerializeField]
    Transform target;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;

        //EventDelegateManager.instance.restartLevelDelegate += UpdatePosition;

    }
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        // has problem with execute order
        transform.position = offset + target.position;
    }
}
