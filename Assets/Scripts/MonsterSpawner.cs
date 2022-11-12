using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject monsterPrefab;

    [SerializeField]
    Transform myPos;

    [SerializeField]
    float minDistanceSpawn = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 randomPos = new Vector3(Random.Range(-75, 75), 0, Random.Range(-75, 75));

        if (randomPos.x < -minDistanceSpawn)
        {
            randomPos.x = -minDistanceSpawn;
        }
        else if (randomPos.x < minDistanceSpawn)
        {
            randomPos.x = minDistanceSpawn;
        }

        if (randomPos.z < -minDistanceSpawn)
        {
            randomPos.z = -minDistanceSpawn;
        }
        else if (randomPos.z < minDistanceSpawn)
        {
            randomPos.z = minDistanceSpawn;
        }


        if (Random.Range(0,100) < 2)
        {
            Instantiate(monsterPrefab, myPos.position + randomPos, Quaternion.identity);
        }


    }
}
