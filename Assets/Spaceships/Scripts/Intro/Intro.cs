using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public GameObject SpaceshipsTextObject;
    public AudioSource audio;

    // Use this for initialization
    void Start ()
    {
        //audio = GetComponent<AudioSource>();
        audio.Play();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (SpaceshipsTextObject.transform.localRotation.eulerAngles.y <= 180)
        {
            SpaceshipsTextObject.transform.Rotate(Vector3.up, 30 * Time.deltaTime);
        }
        if (!audio.isPlaying)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}
