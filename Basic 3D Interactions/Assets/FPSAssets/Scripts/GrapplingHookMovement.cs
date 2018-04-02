using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookMovement : MonoBehaviour
{
    //GrapplingHook childHookData;
    HandGunDamage childInputData;
    Rigidbody playerRigidBody;
    public float pullForce = 1f;

	// Use this for initialization
	void Start ()
    {
        //childHookData = GetComponentInChildren<GrapplingHook>();
        playerRigidBody = GetComponentInParent<Rigidbody>();
        childInputData = GetComponentInChildren<HandGunDamage>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        /*
		if(Input.GetButton("Fire2") && childHookData.hookEnd.GetComponent<GrapplingHook>().hookIsAttached)
        {
            playerRigidBody.isKinematic = false;
            playerRigidBody.AddForce((childHookData.grapplePoint - GetComponentInParent<Transform>().position) * pullForce);
        }*/
        if (Input.GetButton("Fire2") && childInputData.grappleDistance != -1)
        {
            playerRigidBody.isKinematic = false;
            playerRigidBody.AddForce((childInputData.grappleEnd.point - GetComponentInParent<Transform>().position) * pullForce);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            playerRigidBody.isKinematic = true;
            childInputData.grappleDistance = -1;
        }
	}
}
