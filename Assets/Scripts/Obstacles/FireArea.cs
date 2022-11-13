using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{

    private float damage = 1.0f;
    private float fire_last_time;
    private float fire_time_period = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision)
        {
            if (Time.time - fire_last_time > fire_time_period)
            {
                try
                {
                    contact.otherCollider.GetComponent<HP>().Damage(damage);
                    fire_last_time = Time.time;
                }
                catch (System.Exception)
                {
                    //throw;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
