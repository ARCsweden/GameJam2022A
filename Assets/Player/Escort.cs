using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escort : MonoBehaviour
{
    public GameObject TargetCastle;
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float groundedTimer;
    public float playerSpeed = 8.0f;
    private float gravityValue = 9.81f;

    public GameObject escortPrefab;
    public GameObject[] players;
    private Vector3 escortGoal;

    private float playersSumX = 0, playersSumZ = 0;
    private float playerMaxDist = 0;


    Vector3 CameraStartPos;

    [SerializeField]
    Transform cameraPosition;

    private void Start()
    {
        CameraStartPos = cameraPosition.localPosition;
        controller = gameObject.GetComponent<CharacterController>();

        findFurthestCastle();
    }

    void findFurthestCastle()
    {
        GameObject[] FindCastles = GameObject.FindGameObjectsWithTag("Castle");
        float FurthestCastle = 999 * 999;

        foreach (GameObject castle in FindCastles)
        {
            if (Vector3.Distance(castle.transform.position,gameObject.transform.position) < FurthestCastle)
            {
                TargetCastle = castle;
            }
        }

    }

    void Update()
    {
        if (TargetCastle == null)
        {
            findFurthestCastle();
        }

        GameObject[] getAllPlayers = GameObject.FindGameObjectsWithTag("Player");
        int alivePlayers = 0;


        foreach (var item in getAllPlayers)
        {
            if (item.GetComponent<Player>().Alive)
            {
                alivePlayers++;
            }
        }
        int arrayCounter = 0;

        if (alivePlayers>0)
        {
            players = new GameObject[alivePlayers];

            foreach (var item in getAllPlayers)
            {
                if (item.GetComponent<Player>().Alive)
                {
                    alivePlayers++;
                    players[arrayCounter] = item;
                    arrayCounter++;
                }
            }
        }

        //players = GameObject.FindGameObjectsWithTag("Player");

        playersSumX = 0;
        playersSumZ = 0;
        playerMaxDist = 0.0f;
        foreach (GameObject player in players)
        {
            Vector3 player_pos = player.transform.position;

            playersSumX += player_pos.x;
            playersSumZ += player_pos.z;

            if (playerMaxDist < Vector3.Distance(player_pos, controller.transform.position))
            {
                playerMaxDist = Vector3.Distance(player_pos, controller.transform.position);
            }
        }

        //Debug.Log("Max distance: " + playerMaxDist);

        if (players.Length > 0)
        {
            escortGoal.x = playersSumX / players.Length;
            escortGoal.z = playersSumZ / players.Length;
            escortGoal.y = 0.0f;

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer)
            {
                groundedTimer = 0.2f;
            }

            if (groundedTimer > 0)
            {
                groundedTimer -= Time.deltaTime;
            }

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0.0f;
            }

            playerVelocity.y += -gravityValue * Time.deltaTime;

            Vector3 move = Vector3.Normalize(escortGoal - controller.transform.position);

            move *= playerSpeed;

            //Debug.Log("Escort Move x: " + move.x * Time.deltaTime + " y: " + move.y * Time.deltaTime + " z: " + move.z * Time.deltaTime);

            controller.Move(move * Time.deltaTime);
        }


        cameraPosition.localPosition = new Vector3(CameraStartPos.x, Mathf.Max(CameraStartPos.y, CameraStartPos.y + playerMaxDist) ,CameraStartPos.z);

    }
}
