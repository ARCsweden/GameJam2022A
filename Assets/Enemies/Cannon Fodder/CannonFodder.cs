using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFodder : MonoBehaviour
{

    [SerializeField]
    Transform myPosition;

    [SerializeField]
    Rigidbody myRigidBody;

    GameObject[] players;
    GameObject[] escort;
    GameObject target;

    float Distance;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        escort = GameObject.FindGameObjectsWithTag("Escort");
    }

    // Update is called once per frame
    void Update()
    {
        Distance = 1000;
        FindClosestTarget();

        myPosition.LookAt(target.GetComponent<Transform>().position);
        myPosition.rotation.SetEulerAngles(new Vector3(-90,myPosition.rotation.eulerAngles.y,myPosition.rotation.eulerAngles.z));
        myRigidBody.velocity = myPosition.forward * 2;
    }

    void FindClosestTarget()
    {
        float currentDistance = 0;
        foreach (var item in players)
        {
            currentDistance = Vector3.Distance(myPosition.position, item.GetComponent<Transform>().position);
            if (currentDistance < Distance)
            {
                target = item;
            }
        }

        foreach (var item in escort)
        {
            currentDistance = Vector3.Distance(myPosition.position, item.GetComponent<Transform>().position);
            if (currentDistance < Distance)
            {
                target = item;
            }
        }

    }

}
