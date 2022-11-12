using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float groundedTimer;
    public float playerSpeed = 8.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;

    private float inputMoveValueX = 0, inputMoveValueY = 0, inputMoveValueZ = 0;
    private float inputRotValueX = 0, inputRotValueY = 0, inputRotValueZ = 0;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            groundedTimer = 0.2f;
        }

        if(groundedTimer > 0){
            groundedTimer -= Time.deltaTime;
        }

        if(groundedPlayer && playerVelocity.y < 0){
            playerVelocity.y = 0.0f;
        }

        playerVelocity.y += -gravityValue * Time.deltaTime;

        Vector3 move = new Vector3(inputMoveValueX, 0, inputMoveValueZ);
        Vector3 rot = new Vector3(inputRotValueX, 0, inputRotValueZ);

        move *= playerSpeed;

        if (rot != Vector3.zero)
            gameObject.transform.forward = rot;

        if(inputMoveValueY > 0){
            if(groundedTimer > 0){
                groundedTimer = 0.0f;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }

        if(float.IsNaN(playerVelocity.y))
            move.y = 0.0f;
        else
            move.y = playerVelocity.y;

        //Debug.Log("Move x: " + move.x * Time.deltaTime + " y: " + move.y * Time.deltaTime + " z: " + move.z * Time.deltaTime);

        controller.Move(move * Time.deltaTime);
    }

    void OnMovement(InputValue input)
    {
        Vector2 getInputLeftStick = input.Get<Vector2>();
        Debug.Log("Move Left Stick - X: " + getInputLeftStick.x + "     Z: " + getInputLeftStick.y);
        inputMoveValueX = getInputLeftStick.x;
        inputMoveValueZ = getInputLeftStick.y;
    }

    void OnRotation(InputValue input)
    {
        Vector2 getInputRightStick = input.Get<Vector2>();
        Debug.Log("Rot Right Stick - X: " + getInputRightStick.x + "     Z: " + getInputRightStick.y);
        inputRotValueX = getInputRightStick.x;
        inputRotValueZ = getInputRightStick.y;
    }

    void OnJump(InputValue input)
    {
        float getInputSouthButton = input.Get<float>();
        Debug.Log("South Button: " + getInputSouthButton);
        inputMoveValueY = getInputSouthButton;
    }
}
