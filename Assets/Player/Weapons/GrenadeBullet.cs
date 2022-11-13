using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField]
    Rigidbody myRig;
    private float time_start;
    private float bullet_lifetime = 3.0f;
    public float baseSpeed = 10.0f;
    public float levelSpeed = 0f;
    public float baseDamage = 1.0f;
    public float levelDamage = 1.0f;
    public float baseArea = 1.0f;
    public float levelArea = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        int level = GetComponentInParent(typeof(Player)).Level;
        float speed = baseSpeed + levelSpeed * level;

        //controller = gameObject.GetComponent<CharacterController>();
        time_start = Time.time;
        myRig.AddForce(gameObject.transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, bullet_lifetime);

    }

    void OnCollisionEnter(Collision collision)
    {
        int level = GetComponentInParent(typeof(Player)).Level;

        float damage = baseDamage + levelDamage * level;
        float area = baseArea + levelArea * level;

        //ContactPoint contact = collision.contacts[0];
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, area, new Vector3(0, 0, 0), 0);

        try
        {

            //contact.otherCollider.GetComponent<HP>().Damage(damage);
            foreach (RaycastHit hit in hits)
            {
                hit.collider.GetComponent<HP>().Damage(damage);
            }


        }
        catch (System.Exception)
        {

            //throw;
        }
        Destroy(gameObject);
    }
}
