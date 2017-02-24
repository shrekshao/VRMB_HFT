using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPlayerScript : MonoBehaviour {


    public float rotationSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    public float moveFriction = 0.95f;
    //public float shakeThreshold = 20.0f;

    public float moveThreshold = 1.0f;

    private HFTGamepad m_gamepad;
    private HFTInput m_hftInput;

    private float speed = 0.0f;

    // mobile phone (controller) rotation
    //private Quaternion baseRotation;
    private Quaternion inverseBaseRotation;
    private Quaternion lastRotation;    

    private static int s_playerCount = 0;


    // Use this for initialization
    void Start () {
        m_gamepad = GetComponent<HFTGamepad>();
        m_hftInput = GetComponent<HFTInput>();

        int playerNdx = s_playerCount++;
        //transform.position = new Vector3(
        //    CenterOut(playerNdx % 9) * 2.5f,
        //    CenterOut(playerNdx / 9 % 5) * 2.5f,
        //    transform.position.z);

        //SetName(m_gamepad.Name);
        SetColor(m_gamepad.Color);

        // Delete ourselves if disconnected
        m_gamepad.OnDisconnect += Remove;


        //baseRotation = Quaternion.identity;
        inverseBaseRotation = Quaternion.identity;
        lastRotation = transform.rotation;
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

            float baseY = m_hftInput.gyro.attitude.eulerAngles.y;
            //baseRotation = Quaternion.Euler(0, baseY, 0);

            //inverseBaseRotation = Quaternion.Euler(0, -baseY, 0);
            inverseBaseRotation = Quaternion.Inverse(m_hftInput.gyro.attitude);


            //lastRotation = Quaternion.Euler(0, baseY, 0);
            lastRotation = m_hftInput.gyro.attitude;
            transform.rotation = Quaternion.identity;
            //transform.rotation = m_hftInput.gyro.attitude;
            //Debug.Log("set swrod base position from EulerY=" + baseY);
        }
        else
        {
            //Quaternion q = Quaternion.Slerp(transform.rotation, m_hftInput.gyro.attitude, rotationSpeed * Time.deltaTime);
            //transform.rotation = q;


            //Quaternion q = Quaternion.Slerp(lastRotation, m_hftInput.gyro.attitude, rotationSpeed * Time.deltaTime);
            Quaternion q = m_hftInput.gyro.attitude;
            lastRotation = m_hftInput.gyro.attitude;
            //transform.rotation = q * inverseBaseRotation;
            transform.rotation = inverseBaseRotation * q;

            
        }



        // translate

        //if ( m_hftInput.gyro.userAcceleration.x > moveThreshold )

        //if ( m_hftInput.acceleration.x > moveThreshold )
        //{
        //    transform.Translate( new Vector3(m_hftInput.acceleration.x * 0.01f, 0f, 0f) );
        //}
  

        //m_hftInput.gyro.userAcceleration.z

        //transform.Translate(Vector3.up * Time.deltaTime * speed);

    }



    private float CenterOut(int v)
    {
        if (v == 0)
        {
            return (float)v;
        }
        return (float)((v + 1) / 2 * ((v & 1) == 0 ? 1 : -1));
    }

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
