using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject SetBaseRotationUI;

    [SerializeField]
    GameObject DeathUI;


    // Use this for initialization
    void Start () {
        EventDelegateManager.instance.restartLevelDelegate += HideDeathUI;
        EventDelegateManager.instance.playerDieDelegate += ShowDeathUI;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void ShowDeathUI()
    {
        DeathUI.SetActive(true);
    }

    void HideDeathUI()
    {
        DeathUI.SetActive(false);
    }
}
