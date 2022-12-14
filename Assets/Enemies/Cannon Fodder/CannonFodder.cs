using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFodder : MonoBehaviour
{
    [SerializeField]
    float Damage = 1;

    [SerializeField]
    Transform myPosition;

    //[SerializeField]
    public float Speed;


    [SerializeField]
    Rigidbody myRigidBody;

    GameObject[] players;
    GameObject[] escort;
    GameObject target;

    float Distance;

    // Start is called before the first frame update
    void Start()
    {
       
        escort = GameObject.FindGameObjectsWithTag("Escort");
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        Distance = 1000;
        FindClosestTarget();

        myPosition.LookAt(target.GetComponent<Transform>().position);
        myPosition.rotation.SetEulerAngles(new Vector3(-90,myPosition.rotation.eulerAngles.y,myPosition.rotation.eulerAngles.z));
        myRigidBody.velocity = myPosition.forward * Speed;
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


    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        if (contact.otherCollider.tag == "Player" || contact.otherCollider.tag == "Escort")
        {
            try
            {
                contact.otherCollider.GetComponent<HP>().Damage(Damage);
            }
            catch (System.Exception)
            {
                //throw;
            }
        }
    }

}
