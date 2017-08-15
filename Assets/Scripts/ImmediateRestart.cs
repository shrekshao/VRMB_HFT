using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmediateRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventDelegateManager.instance.restartLevelDelegate();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
