using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public CharacterController controller;
    public float turnSpeed;
    public Animator animator;
    private Vector3 inputVector = new Vector3(0, 0, 0);
    private float turnVelocity;
    public bool midRoll;
    private Vector3 currentVelocity;

    public PlayerHealth playerHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        midRoll = false;
        currentVelocity = Vector3.zero;
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementCheck();
        StartCoroutine(RollCheck());
    }

    void MovementCheck() 
    {
        if (midRoll == false)
        {
            //Creates a vector based on player inputs, X value for horizontal inputs (A/D) and Z value for vertical (W/S)
            Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            currentVelocity = inputVector * speed * Time.deltaTime;
            //Move the character based on the input
            controller.Move(currentVelocity);

            //This handles rotation, only runs if they're inputting movement. This is to stop it resetting the character into facing up.
            if (inputVector.x != 0 || inputVector.z != 0) 
            {
                //Gets the angle from the character is moving in, converts it to degrees. I modified a few lines from Brackeys for this. Source: https://www.youtube.com/watch?v=4HpC--2iowE
                float angle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;
                //Smoothens the turning instead of instantly snapping to the angle
                float turningAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, turnSpeed);
                //Sets the character rotation to the angle
                transform.rotation = Quaternion.Euler(0f, turningAngle, 0f);

                animator.SetBool("IsMoving", true);
            } else
            {
                animator.SetBool("IsMoving", false);
            }
        } //This keeps the player moving in their current direction if they are rolling to prevent them sliding around.
        else 
        {
            controller.Move(currentVelocity);
        }
    }

    IEnumerator RollCheck()
    {
        //Checks for player input and that they are not already mid-roll
        if (Input.GetKeyDown(KeyCode.Space) && midRoll == false) 
        {
            //Debug.Log("Great success!");
            midRoll = true;
            animator.SetBool("startRoll", true);
            playerHealth.GetComponent<PlayerHealth>().SetInvuln(true);
            speed = speed * 1.1f;
            yield return new WaitForSecondsRealtime(1f);
            speed = speed / 1.1f;
            playerHealth.GetComponent<PlayerHealth>().SetInvuln(false);
            animator.SetBool("startRoll", false);
            midRoll = false;
        } 
    }

}
