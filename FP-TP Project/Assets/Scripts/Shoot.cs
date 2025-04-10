using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    /// <summary>
    /// Gun can be shot at any frame.
    /// </summary>
	void Update () {
        FireGun();
	}


    /// <summary>
    /// Use weapon when input Fire1 is called. 
    /// Plays sound effect at every shot.
    /// </summary>
    public void FireGun()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
            GetComponent<AudioSource>().Play();
        }
    }

    //PROPERTIES
    [SerializeField] private GameObject bullet;
    [SerializeField] private float firingSpeed = 30.0f;
    [SerializeField] private GameObject gunEnd; //Adding this fixed issue of next-to-wall instantiation bug

    /// <summary>
    /// Two types of bullets: foam bullet and metal bullet. 
    /// Serialized prefab bullet is assigned in the inspector depending on which gun it is ("nerf gun" or "minigun").
    /// Gun instantiates bullet at the gunEnd's location, then assigns a forward movement and speed the bullet's velocity.
    /// Bullet is destroyed after 3 seconds.
    /// </summary>
    public void ShootBullet()
    {
        //Here, the gameObject is the Player Camera
        if (gameObject.tag == "nerf gun")
        {
            //new GameObject foamBullet is created
            GameObject foamBullet = Instantiate(bullet, gunEnd.transform.position, gunEnd.transform.rotation);
            foamBullet.GetComponent<Rigidbody>().linearVelocity = transform.TransformDirection(Vector3.forward * firingSpeed);
            
            Destroy(foamBullet, 3.0f);
        }

        if (gameObject.tag == "minigun")
        {
            //shoot metal bullets from gun
            //remove bullet
        }
    }
}
