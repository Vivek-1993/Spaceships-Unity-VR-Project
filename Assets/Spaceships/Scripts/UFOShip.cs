using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShip : MonoBehaviour
{
    public GameObject shotPrefab;
    public GameObject transformSpaceship;
    public GameObject explosionPrefab;
    public AudioClip audioExplosion;

    float timeStamp;
    float shootCooldown = 1f; // In Seconds

    // Destroys the object when it gets shot by spaceship's laser
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Laser")
        {
            Destroy(this.gameObject);
            GameObject explosion = GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
            AudioSource.PlayClipAtPoint(audioExplosion, transformSpaceship.transform.position);
            Destroy(col.gameObject);
        }
    }

    // If the ship collides with spaceship's collision radius this becomes true and false if it leaves the radius
    bool playerInBounds;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpaceshipCollider")
        {
            playerInBounds = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SpaceshipCollider")
        {
            playerInBounds = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpaceshipCollider")
        {
            playerInBounds = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        transformSpaceship = GameObject.FindGameObjectWithTag("Rig");
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If player is in bounds the spacship periodically fires lasers at spaceship reducing health of spaceship
        //Debug.Log(playerInBounds);
        if (playerInBounds)
        {
            //Debug.Log("Player in Bounds");
            if (timeStamp <= Time.time)
            {
                transform.LookAt(transformSpaceship.transform);
                GameObject laser = GameObject.Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;
                GameObject.Destroy(laser, 0.5f);
                timeStamp = Time.time + shootCooldown;
            }
        }
    }
}