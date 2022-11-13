using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingArea : MonoBehaviour
{
    private float movement_speed = 1.0f;
    private float player_speed_original, escort_speed_original, enemy_speed_original;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_speed_original = collision.gameObject.GetComponent<Player>().playerSpeed;
            collision.gameObject.GetComponent<Player>().playerSpeed = movement_speed;
        }
        else if (collision.gameObject.CompareTag("Escort"))
        {
            escort_speed_original = collision.gameObject.GetComponent<Escort>().playerSpeed;
            collision.gameObject.GetComponent<Escort>().playerSpeed = movement_speed;
        }
        /*  else if (collision.gameObject.CompareTag("Enemy")){
             enemy_speed_original = collision.gameObject.GetComponent<CannonFodder>().playerSpeed;
             collision.gameObject.GetComponent<CannonFodder>().playerSpeed = movement_speed;
         }  */
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().playerSpeed = player_speed_original;
        }
        else if (collision.gameObject.CompareTag("Escort"))
        {
            collision.gameObject.GetComponent<Escort>().playerSpeed = escort_speed_original;
        }
        /* else if (collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<CannonFodder>().playerSpeed = enemy_speed_original;
        } */
    }
    // Update is called once per frame
    void Update()
    {

    }
}
