using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Points to the star objective at the end of the game
    public GameObject star;

	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(star.transform);
    }
}
