using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private CharacterController controller;
    [SerializeField]
    Rigidbody myRig;
    private float time_start;
    private float bullet_lifetime = 3.0f;
    public float bullet_speed = 10.0f;
    [SerializeField]
    float bullet_damage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        time_start = Time.time;
        myRig.AddForce(gameObject.transform.forward * bullet_speed, ForceMode.Impulse);
        Destroy(gameObject, bullet_lifetime);

    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        try
        {
            contact.otherCollider.GetComponent<HP>().Damage(bullet_damage);
            
        }
        catch (System.Exception)
        {

            //throw;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
