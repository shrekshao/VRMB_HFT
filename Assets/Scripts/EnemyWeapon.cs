﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    public int damage = 2;

    void OnTriggerEnter( Collider other )
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit player");
            other.GetComponent<PlayerCharacter>().applyDamage(-damage);
        }
    }
}
