using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunDamage : MonoBehaviour {

    public int damageAmount = 5;
    public float allowedRange = 15;
    public float targetDistance;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit shot;
            
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
            {
                targetDistance = shot.distance;
                if(targetDistance < allowedRange)
                {
                    shot.transform.SendMessage("DeductPoints", damageAmount, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
