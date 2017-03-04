using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameZoneExitDestroy : MonoBehaviour {

    private Collider gameZone;
    // Use this for initialization
    void Start()
    {
        gameZone = GameObject.Find("GameZone").GetComponent<Collider>();
    }

    //// Update is called once per frame
    //void Update () {

    //}

    void OnTriggerExit(Collider other)
    {
        if (other == gameZone)
        {
            Destroy(gameObject);
        }
    }
}
