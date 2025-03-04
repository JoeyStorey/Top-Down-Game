using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public CharacterController controller;
    public float turnSpeed;
    private Vector3 inputVector = new Vector3(0, 0, 0);
    private float turnVelocity;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementCheck();
        RollCheck();
    }

    void MovementCheck() 
    {
        //Creates a vector based on player inputs, X value for horizontal inputs (A/D) and Z value for vertical (W/S)
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        //Move the character based on the input
        controller.Move(inputVector * speed * Time.deltaTime);

        //This part handles the rotation of the player character. I modified a few lines from Brackeys for this. Source: https://www.youtube.com/watch?v=4HpC--2iowE
        //Gets the angle from the character is moving in, converts it to degrees.
        float angle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;
        //Smoothens the turning instead of instantly snapping to the angle
        float turningAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, turnSpeed);
        //Sets the character rotation to the angle
        transform.rotation = Quaternion.Euler(0f, turningAngle, 0f);

    }

    void RollCheck()
    {

    }

}
