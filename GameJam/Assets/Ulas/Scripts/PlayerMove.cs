using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    public Vector3 velocity;

    public Transform ground;
    public float distance = 0.3f;

    public float speed;
    public float jumpHeight;
    public float gravity;
    public Vector3 currentPosition;
    public Vector3 lastPosition;
    public float currentSpeed;

    public LayerMask mask;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.skinWidth = 0.1f;
    }

    private void Update()
    {
        #region Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.right * horizontal + transform.forward * vertical;
        // Debug.Log(newPosition);
        newPosition = Vector3.ClampMagnitude(newPosition, 1f);


        // Calculate player's speed using the distance travelled during this frame
        controller.Move(newPosition * speed * Time.deltaTime);
        currentPosition = transform.position;
        currentSpeed = CheckGrounded() ? Vector3.Distance(lastPosition, currentPosition) / Time.deltaTime : Mathf.Abs((currentPosition - lastPosition).y / Time.deltaTime);

        // Store the player's current position before moving
        lastPosition = transform.position;
        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) &&CheckGrounded())
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        }
        #endregion

        #region Gravity
        

        if (CheckGrounded() && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        controller.Move(velocity * Time.deltaTime);
        #endregion
        Debug.DrawRay(transform.position, Vector3.down,  Color.red);
    }

   private bool CheckGrounded()
    {
        Debug.Log(Physics.Raycast(transform.position, Vector3.down, 2f));
        return Physics.Raycast(transform.position, Vector3.down, 2f);

    }

}
