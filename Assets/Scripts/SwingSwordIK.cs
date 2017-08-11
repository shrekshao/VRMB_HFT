using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSwordIK : MonoBehaviour {
    protected Animator animator;

    public bool ikActive = true;
    public Transform rightHandObj = null;
    //public Transform lookObj = null;

    public Transform sword;

    private Transform ikHandler;

    void Start()
    {
        animator = GetComponent<Animator>();
        ikHandler = transform.Find("IkHandler");
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                //// Set the look target position, if one has been assigned
                //if (ikHandler != null)
                //{
                //    animator.SetLookAtWeight(1);
                //    animator.SetLookAtPosition(ikHandler.position);
                //}

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    //animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.localRotation);
                    //animator.SetIKRotation(AvatarIKGoal.RightHand, ikHandler.rotation);
                    //animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(0f, 0f, 0f));     // local rotation


                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
    
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
