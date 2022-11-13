using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    int[] costTable;


    [SerializeField]
    TMP_Text pointsText;

    int points;
    float Timer;

    [SerializeField]
    int activeMenuItem;

    [SerializeField]
    RectTransform[] BuySpots;

    [SerializeField]
    RectTransform Marker;

    [SerializeField]
    Canvas playerMenu;

    [SerializeField]
    Transform PlayerTransform;

    bool menuOpen;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float groundedTimer;
    public float playerSpeed = 8.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;

    private float inputMoveValueX = 0, inputMoveValueY = 0, inputMoveValueZ = 0;
    private float inputRotValueX = 0, inputRotValueZ = 0;
    private float inputShoot = 0;

    public GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;

    private void Start()
    {
        costTable = new int[5];
        costTable[0] = 5;
        costTable[1] = 10;
        costTable[2] = 1;
        costTable[3] = 1;
        costTable[4] = 1;

        Timer = 0;
        controller = gameObject.GetComponent<CharacterController>();
        menuOpen = false;
        playerMenu.gameObject.SetActive(false);
        gameObject.GetComponent<CharacterController>().enabled = false;
        PlayerTransform.position = GameObject.FindGameObjectWithTag("Escort").transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        gameObject.GetComponent<CharacterController>().enabled = true;
        activeMenuItem = 0;
    }

    private void FixedUpdate()
    {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            Timer = 0;
            points++;
            pointsText.text = points.ToString();
        }
    }

    void Update()
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("Escort").transform.position);

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

    void OnConfirmOrder()
    {
        if (points > costTable[activeMenuItem])
        {
            points -= costTable[activeMenuItem];
            menuOpen = false;
            playerMenu.gameObject.SetActive(false);
        }
    }

    void OnNavigateMenu(InputValue input)
    {
        float navigate = input.Get<Vector2>().y;

        if (navigate>0)
        {
            if (activeMenuItem == 0)
            {
                activeMenuItem = BuySpots.Length;
            }
            else
            {
                activeMenuItem--;
            }
        }
        else if (navigate < 0)
        {
            if (activeMenuItem == BuySpots.Length)
            {
                activeMenuItem = 0;
            }
            else
            {
                activeMenuItem++;
            }
        }

        Marker.position = BuySpots[activeMenuItem].position;
    }

    void OnOpenShop()
    {
        menuOpen = !menuOpen;

        if (menuOpen)
        {
            playerMenu.gameObject.SetActive(true);
        }
        else
        {
            playerMenu.gameObject.SetActive(false);
        }
    }

    void OnMovement(InputValue input)
    {
        Vector2 getInputLeftStick = input.Get<Vector2>();
        //Debug.Log("Move Left Stick - X: " + getInputLeftStick.x + "     Z: " + getInputLeftStick.y);
        inputMoveValueX = getInputLeftStick.x;
        inputMoveValueZ = getInputLeftStick.y;
    }

    void OnRotation(InputValue input)
    {
        Vector2 getInputRightStick = input.Get<Vector2>();
        //Debug.Log("Rot Right Stick - X: " + getInputRightStick.x + "     Z: " + getInputRightStick.y);
        inputRotValueX = getInputRightStick.x;
        inputRotValueZ = getInputRightStick.y;
    }

    void OnJump(InputValue input)
    {
        float getInputSouthButton = input.Get<float>();
        //Debug.Log("South Button: " + getInputSouthButton);
        inputMoveValueY = getInputSouthButton;
    }

    void OnShoot(InputValue input)
    {
        float getInputWestButton = input.Get<float>();
        //Debug.Log("West Button: " + getInputWestButton);
        inputShoot = getInputWestButton;

        try
        {
            Instantiate(bulletPrefab, bulletSpawn.position, controller.transform.rotation);
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to brappbrapp");
            //throw;
        }
        
    }


}
