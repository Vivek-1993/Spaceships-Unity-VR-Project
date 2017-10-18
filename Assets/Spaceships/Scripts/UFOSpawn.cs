using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawn : MonoBehaviour
{
    public GameObject UFOShip0;
    public GameObject UFOShip1;
    public GameObject UFOShip2;
    public GameObject spaceship;
    float timeStamp;
    float spawnCooldown = 5f; // In Seconds

    void SpawnUFO0()
    {
        //Vector3 spaceshipPosition = spaceship.transform.position;
        //Vector3 spaceshipDirection = spaceship.transform.forward;
        //Quaternion spaceshipRotation = spaceship.transform.rotation;
        //Debug.Log(spaceshipRotation);
        //float spawnDistance = 10; //Random.Range(-20,20);

        //Vector3 spawnPosition = spaceshipPosition + spaceshipDirection * spawnDistance;

        //GameObject UFO = GameObject.Instantiate(UFOShip0, spawnPosition, spaceshipRotation);

        GameObject UFO = GameObject.Instantiate(UFOShip0, new Vector3(Random.Range((spaceship.transform.position.x) - 20.0f, (spaceship.transform.position.x) + 20.0f), spaceship.transform.position.y, Random.Range((spaceship.transform.position.z) - 20.0f, (spaceship.transform.position.z) + 20.0f)), transform.rotation) as GameObject;
    }

    void SpawnUFO1()
    {
        GameObject UFO = GameObject.Instantiate(UFOShip1, new Vector3(Random.Range((spaceship.transform.position.x) - 20.0f, (spaceship.transform.position.x) + 20.0f), spaceship.transform.position.y, Random.Range((spaceship.transform.position.z) - 20.0f, (spaceship.transform.position.z) + 20.0f)), transform.rotation) as GameObject;
    }

    void SpawnUFO2()
    {
        GameObject UFO = GameObject.Instantiate(UFOShip2, new Vector3(Random.Range((spaceship.transform.position.x) - 20.0f, (spaceship.transform.position.x) + 20.0f), spaceship.transform.position.y, Random.Range((spaceship.transform.position.z) - 20.0f, (spaceship.transform.position.z) + 20.0f)), transform.rotation) as GameObject;
    }
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Periodically spawns a random UFO within a radius around the spaceship
        if (timeStamp <= Time.time)
        {

            SpawnUFO0();

            int shipSelect = Random.Range(0, 3);

            if (shipSelect == 0)
            {
                SpawnUFO0();
            }
            else if (shipSelect == 1)
            {
                SpawnUFO1();
            }
            else if (shipSelect == 2)
            {
                SpawnUFO2();
            }
            timeStamp = Time.time + spawnCooldown;
        }
    }
}