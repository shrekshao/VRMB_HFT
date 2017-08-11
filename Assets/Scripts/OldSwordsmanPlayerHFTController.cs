using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSwordsmanPlayerHFTController : MonoBehaviour {

    //[SerializeField]
    //public Transform sword;
    //[SerializeField]
    //public Transform ikHandler;

    private Transform sword;
    private Transform ikHandler;
    private Transform hand;

    public float rotationSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    public float moveFriction = 0.95f;


    private HFTGamepad m_gamepad;
    private HFTInput m_hftInput;

    // TODO: camera base rotation
    //private Quaternion cameraBaseRotation;
    private Quaternion inverseBaseRotation;     // base quaternion of controller (used for swords)
    private Vector3 baseEulerAngles;            // base euler angles of controller (used for ikhandler)
    //private Quaternion lastRotation;

    private static int s_playerCount = 0;

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
        m_gamepad.OnDisconnect += Remove;



        //cameraBaseRotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f);


        //baseRotation = Quaternion.identity;
        inverseBaseRotation = Quaternion.identity;
        //lastRotation = transform.rotation;
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

            //baseEulerAngles = m_hftInput.gyro.attitude.eulerAngles;
            sword.rotation = Quaternion.identity;
        }
        else
        {

            Quaternion q = m_hftInput.gyro.attitude;
            //sword.rotation = inverseBaseRotation * q;
            sword.rotation = Camera.main.transform.rotation * Quaternion.Euler(0f, -180f, 0f)
                            * inverseBaseRotation * q;



            //Vector3 deltaEulerAngles = q.eulerAngles - baseEulerAngles;

            //ikHandler.eulerAngles = q.eulerAngles - baseEulerAngles;

            //ikHandler.rotation = inverseBaseRotation * q;
            ikHandler.rotation = sword.rotation;
        }

        sword.position = hand.position;

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
