using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var gunSound = GetComponent<AudioSource>();
            gunSound.Play();
            GetComponent<Animation>().Play("GunShot");
        }

	}
}
