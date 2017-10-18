using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTiltLever : MonoBehaviour
{
    public GameObject verticalTiltLeverObject;
    public GameObject spaceshipRigObject;

    float verticalTiltLowestPosition = 2.0f;
    float verticalTiltHighestPostion = 2.5f;
    float tiltPosition;
    float spaceshipRigTiltAngle;

    float posX;
    float posZ;

    // Use this for initialization
    void Start ()
    {
        //initial positions of the lever at the start of the game
        //used to hold lever fixed in certain directions
        posX = verticalTiltLeverObject.transform.localPosition.x;
        posZ = verticalTiltLeverObject.transform.localPosition.z;

    }

    // Update is called once per frame
    void Update ()
    {
        if (verticalTiltLeverObject.transform.localPosition.y > verticalTiltHighestPostion)
        {
            verticalTiltLeverObject.transform.localPosition = new Vector3(posX, verticalTiltHighestPostion, posZ);
        }
        else if (verticalTiltLeverObject.transform.localPosition.y < verticalTiltLowestPosition)
        {
            verticalTiltLeverObject.transform.localPosition = new Vector3(posX, verticalTiltLowestPosition, posZ);
        }

        tiltPosition = verticalTiltLeverObject.transform.localPosition.y - verticalTiltLowestPosition - 0.25f;
        spaceshipRigTiltAngle = tiltPosition * 180;
        spaceshipRigObject.transform.transform.localEulerAngles = new Vector3(spaceshipRigTiltAngle, spaceshipRigObject.transform.localEulerAngles.y, spaceshipRigObject.transform.localEulerAngles.z);
    }

    void LateUpdate()
    {
        verticalTiltLeverObject.transform.localEulerAngles = new Vector3(0, 90, 0);
        verticalTiltLeverObject.transform.localPosition = new Vector3(posX, verticalTiltLeverObject.transform.localPosition.y, posZ);
    }
}
