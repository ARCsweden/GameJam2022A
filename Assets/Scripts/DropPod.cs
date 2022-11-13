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
            Destroy(dropodRB);
            dropodTrans.position = dropodTrans.position + Vector3.down*2;

            try
            {
                if (!Payload.GetComponent<Player>().Alive)
                {
                    GameObject payload = Payload;
                    payload.GetComponent<HP>().HealthPoints = 1;

                    payload.GetComponent<CharacterController>().enabled = false;
                    Debug.Log("moving player to" + dropodTrans.position + new Vector3(0, 3, 0));
                    payload.transform.position = dropodTrans.position + new Vector3(0, 3, 0);

                    payload.GetComponent<CharacterController>().enabled = true;
                    payload.GetComponent<Player>().enabled = true;
                    payload.GetComponent<Player>().Alive = true;

                    Debug.Log("PLAYER POSITION: " + payload.transform.position);
                }
            }
            catch (System.Exception)
            {
                GameObject payload = Instantiate(Payload);
                payload.transform.position = dropodTrans.position + new Vector3(0, 3, 0);
                //throw;
            }
            Landed = true;
        }
    }
}
