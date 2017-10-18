using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLever : MonoBehaviour
{
    public GameObject speedLeverObject;
    public GameObject spaceshipRigObject;
    public GameObject speedometerPointerObject;

    public AudioSource hittingSound;
    public AudioSource thrustSound;
    public AudioSource speedUpSound;
    public AudioSource slowDownSound;

    public AudioClip hitSoundClip;
    public AudioClip speedUpSoundClip;
    public AudioClip slowDownSoundClip;

    // The lowest limit the speed level can move to in Y axis
    float speedLeverLowestPosition = 2.0f;
    // The highest limit the speed lever can move to in Y axis
    float speedLeverHighestPostion = 2.5f;
    float movementSpeed;
    // Deals with the angle of the pointer of the speedometer
    float speedometerAngle;

    float posX;
    float posZ;

    // This is to prevent sound playing constantly when only want to play once when condition is right
    bool hittingSoundCanPlay = false;
    bool speedUpSoundCanPlay = true;
    bool slowDownSoundCanPlay = false;


    //bool grabLeft = false;
    //bool grabRight = false;

    //public void GrabLeft()
    //{
    //    Debug.Log("Grab Left");
    //    grabLeft = true;
    //}

    //public void ReleaseLeft()
    //{
    //    Debug.Log("Release Left");
    //    grabLeft = false;
    //}

    //public void GrabRight()
    //{
    //    Debug.Log("Grab Right");
    //    grabRight = true;
    //}

    //public void ReleaseRight()
    //{
    //    Debug.Log("Release Right");
    //    grabRight = false;
    //}

    // Collision sound when object collides with speed lever
    void OnCollisionEnter(Collision collision)
    {
        if (!hittingSound.isPlaying && hittingSoundCanPlay)
        {
            hittingSound.PlayOneShot(hitSoundClip);
            hittingSoundCanPlay = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        hittingSoundCanPlay = true;
    }

    // Use this for initialization
    void Start ()
    {
        // Getting initial positions of the lever at the start of the game
        // Used to hold lever fixed in certain directions
        posX = speedLeverObject.transform.localPosition.x;
        posZ = speedLeverObject.transform.localPosition.z;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Controls where the lever is allowed to move to
        // Prevents lever flying off above upper limit position
		if (speedLeverObject.transform.localPosition.y > speedLeverHighestPostion)
        {
            speedLeverObject.transform.localPosition = new Vector3(speedLeverObject.transform.localPosition.x, speedLeverHighestPostion, speedLeverObject.transform.localPosition.z);
        }
        // Prevents lever flying off below lower limit position
        else if (speedLeverObject.transform.localPosition.y < speedLeverLowestPosition)
        {
            speedLeverObject.transform.localPosition = new Vector3(speedLeverObject.transform.localPosition.x, speedLeverLowestPosition, speedLeverObject.transform.localPosition.z);
        }
        // Setting movement speed based on position of lever
        movementSpeed = speedLeverObject.transform.localPosition.y - speedLeverLowestPosition;
        //Debug.Log("movementSpeed" + movementSpeed);

        // Playing sound based on movement speed
        if (movementSpeed > 0.25)
        {
            slowDownSoundCanPlay = true;
            if (!thrustSound.isPlaying)
            {
                thrustSound.Play();
            }
            if (speedUpSoundCanPlay)
            {
                speedUpSound.Play();
                speedUpSoundCanPlay = false;
            }
        }
        else if (movementSpeed <= 0.25)
        {
            thrustSound.Stop();
            speedUpSound.Stop();
            speedUpSoundCanPlay = true;
            if (slowDownSoundCanPlay)
            {
                slowDownSound.Play();
                slowDownSoundCanPlay = false;
            }
        }
        //Debug.Log("Move Speed" + movementSpeed);
        // Moving ship based on movementSpeed variable and time
        spaceshipRigObject.transform.position += spaceshipRigObject.transform.forward * Time.deltaTime * movementSpeed * 10;

        //Speedometer
        // The default angle for 0 speed is 120 degrees and it moves in the negative direction as speed increases.
        speedometerAngle = 120 - (movementSpeed * 480);
        //Debug.Log("Speedometer Angle" + speedometerAngle);
        speedometerPointerObject.transform.localEulerAngles = new Vector3(0, 0, speedometerAngle);
    }

    // Holds the lever in position
    void LateUpdate()
    {
        speedLeverObject.transform.localEulerAngles = new Vector3(0, 90, 0);
        speedLeverObject.transform.localPosition = new Vector3(posX, speedLeverObject.transform.localPosition.y, posZ);
    }
}
