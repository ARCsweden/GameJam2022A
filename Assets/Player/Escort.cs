using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escort : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float groundedTimer;
    public float playerSpeed = 8.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;

    public GameObject escortPrefab;
    public GameObject[] players;
    private Vector3 escortGoal;

    private float playersSumX = 0, playersSumZ = 0;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        playersSumX = 0;
        playersSumZ = 0;
        foreach (GameObject player in players)
        {
            playersSumX += player.gameObject.transform.position.x;
            playersSumZ += player.gameObject.transform.position.z;
        }

        if (players.Length > 0)
        {
            escortGoal.x = playersSumX / players.Length;
            escortGoal.z = playersSumZ / players.Length;
            escortGoal.y = 0.0f;

            playerVelocity.y += -gravityValue * Time.deltaTime;

            Vector3 move = escortGoal - controller.transform.position;

            move *= playerSpeed;

            Debug.Log("Escort Move x: " + move.x * Time.deltaTime + " y: " + move.y * Time.deltaTime + " z: " + move.z * Time.deltaTime);

            controller.Move(move * Time.deltaTime);
        }

    }
}
