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

    bool Landed;

    // Start is called before the first frame update
    void Start()
    {
        Landed = false;
        targetPos = GameObject.FindGameObjectWithTag("Escort").transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!Landed)
        {
            dropodTrans.LookAt(targetPos);
            dropodRB.AddForce(dropodTrans.forward * Force, ForceMode.Impulse);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Landed)
        {
            dropodRB.velocity = new Vector3(0, 0, 0);
            dropodRB.angularVelocity = new Vector3(0, 0, 0);

            try
            {
                if (!Payload.GetComponent<Player>().Alive)
                {
                    Payload.transform.position = dropodTrans.position + new Vector3(0, 5, 0);
                    Payload.GetComponent<CharacterController>().enabled = false;
                    Payload.transform.position = dropodTrans.position + new Vector3(0, 5, 0);
                    Debug.Log("moving player to" + dropodTrans.position + new Vector3(0, 5, 0));
                    Payload.GetComponent<Player>().Alive = true;
                    Payload.GetComponent<CharacterController>().enabled = true;
                }
            }
            catch (System.Exception)
            {
                GameObject payload = Instantiate(Payload);
                payload.transform.position = dropodTrans.position + new Vector3(0, 5, 0);
                //throw;
            }
            Landed = true;
        }
    }
}
