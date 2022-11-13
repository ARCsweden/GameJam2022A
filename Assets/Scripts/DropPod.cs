using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPod : MonoBehaviour
{
    [SerializeField]
    float Force;

    public GameObject Payload;

    [SerializeField]
    Rigidbody dropodRB;

    [SerializeField]
    Transform dropodTrans;

    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Escort").transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        dropodTrans.LookAt(targetPos);
        dropodRB.AddForce(dropodTrans.forward* Force,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        dropodRB.velocity = new Vector3(0, 0, 0);
        dropodRB.angularVelocity = new Vector3(0, 0, 0);

        Payload.transform.position = dropodTrans.position + new Vector3(0, 5, 0);
        if (!Payload.GetComponent<Player>().Alive)
        {
            Payload.GetComponent<Player>().Alive = true;
            Payload.GetComponent<CharacterController>().enabled = true;
        }
    }
}
