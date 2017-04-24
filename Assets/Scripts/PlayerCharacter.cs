using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [SerializeField]
    int hpMax = 5;

    int hp;

    BloodScreen m_bloodScreen;

    [SerializeField]
    GameObject armSwordController;

    [SerializeField]
    GameObject parentPlayer;

	// Use this for initialization
	void Start () {
        m_bloodScreen = GetComponent<BloodScreen>();
        hp = hpMax;

        EventDelegateManager.instance.restartLevelDelegate += OnRestartLevel;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void applyDamage(int deltaHP)
    {
        hp += deltaHP;
        m_bloodScreen.SetBloodVisible(true);

        if (hp <= 0)
        {
            executeDeath();
        }
    }

    public void executeDeath()
    {
        Debug.Log("Player die");

        EventDelegateManager.instance.playerDieDelegate();

        //armSwordController.SendMessage("SetDead", true);
        //parentPlayer.GetComponent<MovablePlayer>().enabled = false;
    }

    public void OnRestartLevel()
    {
        hp = hpMax;
    }
}
