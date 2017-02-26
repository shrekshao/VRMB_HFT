using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanPlayerHFTController : MonoBehaviour {
    

    private Transform ikHandler;
    private Transform hand;     // child of ikHandler, an empty pivot object

    public Transform sword;
    private Transform swordEnd;


    public Transform handJoint; // joint of the rigged character
    private Transform handSwordPlaceHolder;
    private Transform handJointUp;


    private Transform debugController;

    private Quaternion handBaseRotation;


    private HFTGamepad m_gamepad;
    private HFTInput m_hftInput;

    // TODO: camera base rotation
    //private Quaternion cameraBaseRotation;
    private Quaternion inverseBaseRotation;     // base quaternion of controller (used for swords)

    private static int s_playerCount = 0;

    // Use this for initialization
    void Start () {
        m_gamepad = GetComponent<HFTGamepad>();
        m_hftInput = GetComponent<HFTInput>();

        ikHandler = transform.FindChild("IkHandler");
        hand = ikHandler.FindChild("Hand");

        swordEnd = sword.FindChild("End");
        handSwordPlaceHolder = handJoint.FindChild("SwordPlaceHolder");
        handJointUp = handJoint.FindChild("Up");

        debugController = GameObject.Find("Capsule").transform;

        int playerNdx = s_playerCount++;


        SetColor(m_gamepad.Color);


        // Delete ourselves if disconnected
        m_gamepad.OnDisconnect += Remove;

        

        //baseRotation = Quaternion.identity;
        inverseBaseRotation = Quaternion.identity;
        //lastRotation = transform.rotation;


        handBaseRotation = Quaternion.Inverse( handJoint.localRotation );
    }


    void Remove()
    {
        Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        // rotation
        if (m_hftInput.GetButtonDown("fire1"))
        {
            // adjust sword 
            //inverseBaseRotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f) 
            //    * Quaternion.Inverse(m_hftInput.gyro.attitude);
            inverseBaseRotation = Quaternion.Inverse(m_hftInput.gyro.attitude);

            hand.localRotation = Quaternion.identity;
            sword.rotation = Quaternion.identity;
        }
        else
        {
            
            Quaternion q = m_hftInput.gyro.attitude;
            //sword.rotation = inverseBaseRotation * q;
            Quaternion r = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f)
                            * inverseBaseRotation * q;

            sword.rotation = r;

            ikHandler.rotation = r;

            sword.position = handSwordPlaceHolder.position;
            //hand.rotation = swordPreQ * r;


            //hand.rotation = handBaseRotation * r;
            //hand.rotation = r;

            //handJoint.rotation = r;
            //handJoint.localRotation = Quaternion.Euler(-90f, 180f, 0f) * r;


            //GetComponent<Animator>().settar





            // set hand joint position
            // rotations are all in world space

            //handJoint.rotation = handJoint.rotation
            //   * Quaternion.FromToRotation(handJointUp.position - handSwordPlaceHolder.position, swordEnd.position - sword.position);

            //handJoint.rotation = handJoint.rotation
            //   * Quaternion.FromToRotation(handSwordPlaceHolder.forward, sword.up);

            //handJoint.rotation = handJoint.rotation
            //   * Quaternion.FromToRotation(debugController.up, sword.up);
            //hand.rotation = debugController.rotation;
            hand.localRotation = Quaternion.Inverse( handJoint.parent.rotation ) * Quaternion.FromToRotation(handSwordPlaceHolder.forward, sword.up);

            //hand.rotation = Quaternion.identity;

            //handJoint.localRotation = Quaternion.identity;
        }

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
