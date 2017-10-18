using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisation : MonoBehaviour
{
    // The help text is hidden after 10 seconds
    float helpTimeStamp;
    float textHide = 10f; // In Seconds
    public GameObject helpText;

    // Use this for initialization
    void Start ()
    {
        helpTimeStamp = Time.time + textHide;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (helpTimeStamp <= Time.time)
        {
            helpText.SetActive(false);
        }
    }
}
