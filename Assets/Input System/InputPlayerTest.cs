using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerTest : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerRB;
    [SerializeField]
    float ForceMultiplier;

    float X;
    float Y;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRB.AddForce(new Vector3(X, 0, Y), ForceMode.Impulse);
    }

    void OnMovement(InputValue input)
    {
        Vector2 getInputLeftThrust = input.Get<Vector2>();
        Debug.Log("Left Stick - X: " + getInputLeftThrust.x* ForceMultiplier + "     Y: " + getInputLeftThrust.y* ForceMultiplier);
        X = getInputLeftThrust.x * ForceMultiplier;
        Y = getInputLeftThrust.y * ForceMultiplier;
    }
}
