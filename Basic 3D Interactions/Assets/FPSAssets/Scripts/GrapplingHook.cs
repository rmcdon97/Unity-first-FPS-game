using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GrapplingHook : MonoBehaviour
{
    public Camera playerCam;
    public RaycastHit hit;
    public float castRadius;

    public LayerMask cullingMask;
    public int maxDistance;

    public bool isFlying;
	public bool isSwinging;
    public Vector3 pos;

    public float speed = 10;
    public Transform hand;

    public FirstPersonController FPC;
    public LineRenderer LR;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            FindSpot();

        if (isFlying)
            Flying();

		if(Input.GetButtonUp("Fire1"))
		{
			isSwinging = false;
			FPC.swinging = false;
			
			if(!Input.GetButton("Fire2"))
			{
				FPC.continueMovementPostGrapple = (pos - transform.position).normalized * speed;
				FPC.grappleReleased = true;
				isFlying = false;
				FPC.canMove = true;
				LR.enabled = false;
			}
		}
		
        if(isFlying && Input.GetButtonUp("Fire2") && !isSwinging)
        {
            FPC.continueMovementPostGrapple = (pos - transform.position).normalized * speed;
            //need to convert from change in pos per update to acceleration force
            //FPC.continueMovementPostGrapple = Vector3.Lerp(transform.position, pos, speed / Vector3.Distance(transform.position, pos));
            FPC.grappleReleased = true;
            isFlying = false;
            FPC.canMove = true;
            LR.enabled = false;
        }

        if(isFlying && Input.GetButtonDown("Fire1"))
        {
            //start rope swing physics
			FPC.continueMovementPostGrapple = (pos - transform.position).normalized * speed;
			FPC.grappleReleased = true;
			isSwinging = true;
			FPC.swinging = true;
			FPC.webPoint = pos;
			FPC.maxWebLength = Vector3.Distance(transform.position, pos);
        }
    }

    public void FindSpot()
    {
        if (Physics.SphereCast(playerCam.transform.position, castRadius, playerCam.transform.forward, out hit, maxDistance, cullingMask))
        {
            isFlying = true;
            pos = hit.point;
            FPC.canMove = false;
            LR.enabled = true;
            LR.SetPosition(1, pos);
        }
    }

    public void Flying()
    {
		if(!isSwinging)
		{
			transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime / Vector3.Distance(transform.position, pos));

			if(Vector3.Distance(transform.position, pos) < .5f)
			{
				isFlying = false;
				FPC.canMove = true;
				LR.enabled = false;
			}
		}
		else
			FPC.canMove = true;
		LR.SetPosition(0, hand.position);
    }








    /*
    public int hookSpeedMultiplier = 10;
    public GameObject hookEnd;
    Rigidbody rb, newRigidBodyComponent;
    public bool hookIsAttached = false;
    public Vector3 grapplePoint, directionToPull;

    // new collider needs to be accessable by parent
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
            
        }
        else if (Input.GetButtonUp("Fire2"))
            Destroy(hookEnd);
	}
    private void OnCollisionEnter(Collision collision)
    {
        grapplePoint = collision.transform.position;
        directionToPull = (grapplePoint - GetComponentInParent<Transform>().position).normalized;
        hookIsAttached = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        GetComponent<Rigidbody>().useGravity = false;
    }*/
}
