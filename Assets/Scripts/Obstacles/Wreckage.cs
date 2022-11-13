using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreckage : MonoBehaviour
{
    private int loot_amount = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().points += loot_amount;
            Destroy(gameObject);
        }
    }
}
