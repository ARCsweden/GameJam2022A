using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CastleGeneration : MonoBehaviour
{
    public GameObject castle;

    public int itemsToPlace = 6;
    public float x_spread = 10;
    public float z_spread = 10;
    public float minimunDistance = 2;

    private int toGenerate;
    private float distance;
    private List<Vector3> position = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        toGenerate = itemsToPlace;

        while (toGenerate > 0)
        {
            distance = 10000;
            Vector3 next = new Vector3(UnityEngine.Random.Range(-x_spread, x_spread), 0, UnityEngine.Random.Range(-z_spread, z_spread));

            foreach (Vector3 i in position) {
                distance = Mathf.Min(Vector3.Distance(i, next), distance);
            }

            if (distance >= minimunDistance)
            {
                position.Add(next);
                GameObject clone = Instantiate(castle, next, Quaternion.identity);
                toGenerate--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
