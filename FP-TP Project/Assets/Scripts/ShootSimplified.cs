using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSimplified : MonoBehaviour {

    /// <summary>
    /// Gun can be shot at any frame.
    /// </summary>
    void Update()
    {
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
    /// Simplified version of the ShootBullet method from Shoot, to use in the loading scren, without the scoring and different guns.
    /// </summary>
    public void ShootBullet()
    {
        //new GameObject foamBullet is created
        GameObject foamBullet = Instantiate(bullet, gunEnd.transform.position, gunEnd.transform.rotation);
        foamBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * firingSpeed);
        Destroy(foamBullet, 3.0f);
    }
}
