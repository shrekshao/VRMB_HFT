using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour {

    [SerializeField]
    GameObject arrow;

    // Use this for initialization
    void Start () {
        StartCoroutine(ShootArrow());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ShootArrow()
    {

        while (true)
        {
            yield return new WaitForSeconds(1);

            GameObject arrow = (GameObject)Instantiate(this.arrow, transform.position, Quaternion.identity);

            arrow.GetComponent<Arrow>().InitVelocity(5f * Vector3.up + 15f * transform.forward);
        }

    }
}
