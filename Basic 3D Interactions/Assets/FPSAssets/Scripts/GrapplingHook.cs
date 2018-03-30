using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public int hookSpeedMultiplier = 10;
    GameObject hookEnd;
    Rigidbody rb, newRigidBodyComponent;
    bool hookIsAttached = false;
	
	// NEED TO CONVERT TO USE ONE HEIRARCHY LEVEL HIGHER
	void Update ()
    {
		if(Input.GetButtonDown("Fire2"))
        {
            Vector3 hookStartPosition = (GetComponentInParent<Transform>().position + GetComponentInParent<Transform>().TransformDirection(Vector3.forward).normalized) - new Vector3(0,.5f,0);
            hookEnd = (GameObject)Instantiate(this.gameObject, hookStartPosition, Quaternion.identity);
            newRigidBodyComponent = hookEnd.AddComponent<Rigidbody>();
            SphereCollider newSphereColliderComponent = hookEnd.GetComponent<SphereCollider>();
            newSphereColliderComponent.enabled = true;
            newRigidBodyComponent.mass = .1f;

            //SphereCollider newSphereColliderComponent = hookEnd.AddComponent<SphereCollider>();
            rb = hookEnd.GetComponent<Rigidbody>();
            Vector3 hookDirection = transform.TransformDirection(Vector3.forward).normalized;
            hookDirection *= hookSpeedMultiplier;
            rb.velocity = hookDirection;
        }
        else if(Input.GetButton("Fire2"))
        {
            if(hookIsAttached)
            {

            }
        }
        else if (Input.GetButtonUp("Fire2"))
            Destroy(hookEnd);
	}
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 grapplePoint = collision.transform.position;
        Vector3 directionToPull = (grapplePoint - GetComponentInParent<Transform>().position).normalized;
        hookIsAttached = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        GetComponent<Rigidbody>().useGravity = false;
    }
}
