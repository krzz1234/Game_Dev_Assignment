using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController mController;
    private Animator mAnimator;
    public Vector3 mVelocity;
    public float WalkSpeed   = 1.894965f;
    public float RunSpeed    = 3.603041f;
    public float SprintSpeed = 4.922f;
    private float horizontal_Input;
    private float Vertical_Input;
    private int jump = 0;

    private Transform mCameraPivot;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        mController = GetComponent<CharacterController>();
        mAnimator = GetComponent<Animator>();

        // Lock/Hide the Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set Velocity to 0
        mVelocity = Vector3.zero;

        // Get Camera Pivot
        mCameraPivot = transform.Find("CameraPivot");
    }

    private void Jump() {
        // Set Velocity Up
        mVelocity.y = 7.0f;
        mAnimator.SetTrigger("Jump");
    }

    private void Attack() {
        // Trigger Attack Animation
        mAnimator.SetTrigger("Attack");
    }


    // Update is called once per frame
    void Update() {
        // Player Speed
        float targetSpeed = 0.0f;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            if(Input.GetKey(KeyCode.LeftControl)) {
                // Player Walking
                targetSpeed = WalkSpeed;
            } else if(Input.GetKey(KeyCode.LeftShift)) {
                // Player Sprinting
                targetSpeed = SprintSpeed;
            } else {
                // Player Running
                targetSpeed = RunSpeed;
            }
        }

        // Look left/right
        mCameraPivot.Rotate(Vector3.up, Input.GetAxis("Mouse X"), Space.World);
        mCameraPivot.Rotate(Vector3.right, -Input.GetAxis("Mouse Y"), Space.Self);

        // Horizontal Movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        
        // Update Velocity
        mVelocity.x = move.x * targetSpeed;
        mVelocity.z = move.z * targetSpeed;

        if(mController.isGrounded == false) {
            mVelocity.y -= 9.8f * Time.deltaTime;
        } else {
            mVelocity.y = -2f;
            //reset the jump
            jump = 0;
        }

        // Update Animation Parameters
        mAnimator.SetFloat("VelocityX", move.x);
        mAnimator.SetFloat("VelocityZ", move.z);
        mAnimator.SetFloat("MoveSpeed", targetSpeed / 5.0f);
        mAnimator.SetBool("isGrounded", mController.isGrounded);


        // User Pressed Space
        if(Input.GetKeyDown(KeyCode.Space)) {
            // Check for double jump
            if(jump < 2){
                Jump();
                jump++;
            }
        }

         // Move Character
        mController.Move(transform.rotation * mVelocity * Time.deltaTime);

        // User Pressed Left-Mouse Button
        if(Input.GetMouseButtonDown(0)) {
            // Attack
            Attack();
        }
    }
}
