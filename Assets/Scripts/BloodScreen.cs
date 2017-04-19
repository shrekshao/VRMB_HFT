using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScreen : MonoBehaviour {

    bool visible = false;

    [SerializeField]
    Texture blood;

    float alpha = 0f;

    //// instance show and delay invisible version
    //void OnGUI()
    //{
    //    //Debug.Log("ongui");
    //    if (visible)
    //    {
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blood);
    //    }
    //}

    //public void SetBloodVisible(bool v)
    //{

    //    if (visible == false && v == true)
    //    {
    //        StartCoroutine(WaitAndSetInvisible());
    //    }

    //    visible = v;
    //}

    //IEnumerator WaitAndSetInvisible()
    //{
    //    yield return new WaitForSeconds(1);

    //    SetBloodVisible(false);
    //}
    // --------------------------------------------

    public void SetBloodVisible(bool v)
    {
        if (v)
        {
            alpha = 1f;
        }
        else
        {
            alpha = 0f;
        }
        

        visible = v;
    }

    void OnGUI()
    {
        if (visible)
        {
            GUI.color = new Color(1f, 1f, 1f, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blood);
            //Debug.Log(GUI.color);
            

            alpha -= 0.01f;
            if (alpha <= 0f)
            {
                visible = false;
                alpha = 0f;
            }
        }

        //if (visible)
        //{
        //    if (alpha >= 1f)
        //    {
        //        Debug.Log("draw texture");
        //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blood);
        //    }
        //    else
        //    {
        //        Color old = GUI.color;
        //        //GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        //        //GUI.color = new Color(old.r, old.g, old.b, alpha);
        //    }


        //    if (alpha <= 0f)
        //    {
        //        visible = false;
        //        alpha = 0f;
        //    }
        //    else
        //    {
        //        alpha -= 0.001f;
        //    }
        //}
    }

    //void Update()
    //{
    //    if (visible)
    //    {
    //        alpha -= 0.01f;
    //        if (alpha <= 0.00001f)
    //        {
    //            visible = false;
    //            alpha = 0f;
    //        }
    //    }
    //}

}
