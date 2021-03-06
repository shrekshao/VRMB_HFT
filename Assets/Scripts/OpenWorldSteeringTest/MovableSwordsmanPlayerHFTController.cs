﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableSwordsmanPlayerHFTController : MonoBehaviour {

    //[SerializeField]
    //public Transform sword;
    //[SerializeField]
    //public Transform ikHandler;

    private Transform sword;
    private Transform ikHandler;
    private Transform hand;


    private HFTGamepad m_gamepad;
    private HFTInput m_hftInput;


    private static bool recentered = false;
    private static Quaternion inverseBaseRotation = Quaternion.identity;     // base quaternion of controller (used for swords)
    private Vector3 baseEulerAngles;            // base euler angles of controller (used for ikhandler)
    //private Quaternion lastRotation;

    private static int s_playerCount = 0;


    



    // indicating if is steering the horse, test
    //private GameObject rope;
    private GameObject parentPlayer;
    private MovablePlayer movablePlayer;

    private bool steering;
    private Quaternion steeringBaseRotation;

    private bool dead = false;

    [SerializeField]
    Vector3 startingPosition = new Vector3(0f, 2.46f, 0f);


    // Use this for initialization
    void Start()
    {
        m_gamepad = GetComponent<HFTGamepad>();
        m_hftInput = GetComponent<HFTInput>();

        ikHandler = transform.Find("IkHandler");
        //sword = ikHandler.FindChild("SwordParent");
        sword = transform.Find("SwordParent");
        hand = ikHandler.Find("Hand");

        


        int playerNdx = s_playerCount++;


        SetColor(m_gamepad.Color);


        // Delete ourselves if disconnected
        //m_gamepad.OnDisconnect += Remove;



        //cameraBaseRotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f);


        //baseRotation = Quaternion.identity;
        //inverseBaseRotation = Quaternion.identity;
        //lastRotation = transform.rotation;





        // TEST: attach as child of player (horse)
        steering = false;
        //rope = sword.Find("Rope").gameObject;
        parentPlayer = GameObject.Find("Player");
        //transform.SetParent(parentPlayer.transform);
        //transform.position = parentPlayer.transform.FindChild("SwordCharacterPivot").position;
        movablePlayer = parentPlayer.GetComponent<MovablePlayer>();



        EventDelegateManager.instance.playerDieDelegate += OnDie;
        EventDelegateManager.instance.restartLevelDelegate += OnRestartLevel;

    }


    void Remove()
    {
        Destroy(gameObject);
    }

    public void SetDead(bool v)
    {
        dead = v;
    }


    void OnDie()
    {
        dead = true;
    }

    void OnRestartLevel()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!recentered)
        {
            // at the start preparing menu
            // (hft controller the first click doesn't respond? why?

            if (m_hftInput.GetButton("fire1"))
            {
                // adjust sword 
                //inverseBaseRotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f) 
                //    * Quaternion.Inverse(m_hftInput.gyro.attitude);
                inverseBaseRotation = Quaternion.Inverse(m_hftInput.gyro.attitude);

                //baseEulerAngles = m_hftInput.gyro.attitude.eulerAngles;
                sword.rotation = Quaternion.identity;

                //recentered = true;
            }
            else if (m_hftInput.GetButton("fire2"))
            {
                recentered = true;
                parentPlayer.GetComponent<MovablePlayer>().enabled = true;
                EventDelegateManager.instance.restartLevelDelegate();
            }



            // Debug Purpose: start the game without mobile phone 

            if (Input.GetKeyUp(KeyCode.Space))
            {
                recentered = true;
                parentPlayer.GetComponent<MovablePlayer>().enabled = true;
                EventDelegateManager.instance.restartLevelDelegate();
            }
        }
        else if (!dead)
        {
            // during the game
            
            if (m_hftInput.GetButton("fire2"))
            {
                // steering the horse

                if (!steering)
                {
                    // first press
                    // capture current rotation

                    steering = true;
                    steeringBaseRotation = m_hftInput.gyro.attitude;


                    //rope.SetActive(true);
                }
                else
                {
                    
                    float steer = Mathf.DeltaAngle(steeringBaseRotation.eulerAngles.z, m_hftInput.gyro.attitude.eulerAngles.z);

                    // rotate
                    // movablePlayer.Steer( Mathf.Clamp( steer / 45f, -1f, 1f  ) );

                    // translate
                    movablePlayer.Translate( Mathf.Clamp(steer / 30f, -1f, 1f) );
                }



                // movablePlayer.Steer(1.0f);

            }
            else
            {
                steering = false;
                //rope.SetActive(false);
            }
        }
        else
        {
            // at the game over menu

            if (m_hftInput.GetButton("fire1"))
            {
                // restart level
                EventDelegateManager.instance.restartLevelDelegate();
                
                //dead = false;

                //// reset parent player location
                //parentPlayer.transform.position = startingPosition;

                //parentPlayer.GetComponent<MovablePlayer>().enabled = true;

                //parentPlayer.BroadcastMessage("RestartLevel");

                //GameObject levelManager = GameObject.Find("LevelManager");
                //levelManager.SendMessage("UpdatePosition");
                //levelManager.SendMessage("RestartLevel");
            }
        }

        Quaternion q = m_hftInput.gyro.attitude;
        //sword.rotation = inverseBaseRotation * q;
        sword.rotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f)
                        * inverseBaseRotation * q;


        ikHandler.rotation = sword.rotation;
        sword.position = hand.position;

        //// rotation
        //if (m_hftInput.GetButtonDown("fire1"))
        //{
        //    // adjust sword 
        //    //inverseBaseRotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f) 
        //    //    * Quaternion.Inverse(m_hftInput.gyro.attitude);
        //    inverseBaseRotation = Quaternion.Inverse(m_hftInput.gyro.attitude);

        //    //baseEulerAngles = m_hftInput.gyro.attitude.eulerAngles;
        //    sword.rotation = Quaternion.identity;

        //    recentered = true;
        //}
        //else
        //{

        //    Quaternion q = m_hftInput.gyro.attitude;
        //    //sword.rotation = inverseBaseRotation * q;
        //    sword.rotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f)
        //                    * inverseBaseRotation * q;



        //    //Vector3 deltaEulerAngles = q.eulerAngles - baseEulerAngles;

        //    //ikHandler.eulerAngles = q.eulerAngles - baseEulerAngles;

        //    //ikHandler.rotation = inverseBaseRotation * q;
        //    ikHandler.rotation = sword.rotation;
        //}

        //sword.position = hand.position;



        ////float steering = m_hftInput.GetAxis("Horizontal");
        ////if (steering > 0f)
        ////{
        ////    steering = 1.0f;
        ////    movablePlayer.Steer(steering);
        ////}
        ////else if (steering < 0f)
        ////{
        ////    steering = -1.0f;
        ////    movablePlayer.Steer(steering);
        ////}

    }



    //private float CenterOut(int v)
    //{
    //    if (v == 0)
    //    {
    //        return (float)v;
    //    }
    //    return (float)((v + 1) / 2 * ((v & 1) == 0 ? 1 : -1));
    //}

    private void SetColor(Color color)
    {
        //m_color = color;
        //m_renderer.material.color = m_color;
        //Color[] pix = new Color[1];
        //pix[0] = color;
        //Texture2D tex = new Texture2D(1, 1);
        //tex.SetPixels(pix);
        //tex.Apply();
        //m_guiStyle.normal.background = tex;
    }
}
