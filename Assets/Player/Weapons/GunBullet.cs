using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private float time_start;
    private float bullet_lifetime = 3.0f;
    public float baseSpeed = 10.0f;
    public float levelSpeed = 0f;
    public float baseDamage = 1.0f;
    public float levelDamage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        int level = GetComponentInParent<Player>().Level;
        //int level = GetComponentInParent(typeof(Player)).Level;
        float speed = baseSpeed + levelSpeed * level;

        //controller = gameObject.GetComponent<CharacterController>();
        time_start = Time.time;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, bullet_lifetime);

    }

    void OnCollisionEnter(Collision collision)
    {
        int level = GetComponentInParent<Player>().Level;

        float damage = baseDamage + levelDamage + level;

        ContactPoint contact = collision.contacts[0];
        try
        {
            contact.otherCollider.GetComponent<HP>().Damage(damage);

        }
        catch (System.Exception)
        {

            //throw;
        }
        Destroy(gameObject);
    }
}
