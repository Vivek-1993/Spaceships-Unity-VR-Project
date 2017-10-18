using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootLeft : MonoBehaviour
{
    public GameObject LightGreen;
    public GameObject LightRed;
    float timeStamp;
    float buttonCooldown = 0.5f; // In Seconds

    public AudioSource audioClick;
    public AudioSource audioWaiting;
    public AudioSource audioShoot;

    public Transform muzzle;
    public GameObject shotPrefab;

    // Fires the left laser on collision
    void OnCollisionEnter(Collision col)
    {
        // Prevents firing if outside objects collide with it
        if (col.gameObject.tag != "UFOShip2" && col.gameObject.tag != "UFOLaser" && col.gameObject.tag != "Explosion" && col.gameObject.tag != "HealthPack" && col.gameObject.tag != "Rig")
        {
            // Activating and cooldown of button
            if (timeStamp <= Time.time)
            {
                Debug.Log("Collision Left");
                audioClick.Play();
                audioShoot.Play();
                GameObject laser = GameObject.Instantiate(shotPrefab, muzzle.position, muzzle.rotation) as GameObject;
                GameObject.Destroy(laser, 0.5f);

                timeStamp = Time.time + buttonCooldown;
            }
            else if (!audioWaiting.isPlaying && !audioClick.isPlaying)
            {
                audioWaiting.Play();
            }
        }
    }

    void Update()
    {
        if (timeStamp <= Time.time)
        {
            LightGreen.SetActive(true);
            LightRed.SetActive(false);
        }
        else
        {
            LightGreen.SetActive(false);
            LightRed.SetActive(true);
        }
    }
}