using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDelegateManager : MonoBehaviour {

    public static EventDelegateManager instance = null;


    public delegate void PlayerDie();
    public PlayerDie playerDieDelegate;

    public delegate void RestartLevel();
    public RestartLevel restartLevelDelegate;

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
