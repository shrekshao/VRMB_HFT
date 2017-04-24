using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour {

    private MovablePlayer movablePlayer;
    public Vector3 startingPosition = Vector3.zero;


	// Use this for initialization
	void Start () {
        movablePlayer = GetComponent<MovablePlayer>();

        EventDelegateManager.instance.restartLevelDelegate += OnRestartLevel;
        EventDelegateManager.instance.playerDieDelegate += OnDie;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRestartLevel()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.identity;
        movablePlayer.enabled = true;
    }

    void OnDie()
    {
        movablePlayer.enabled = false;
    }
}
