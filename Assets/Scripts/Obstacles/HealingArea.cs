using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{

    private float healing_points = 1.0f;
    private float heal_last_time;
    private float heal_time_period = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision)
        {
            if (Time.time - heal_last_time > heal_time_period)
            {
            try
            {   
                contact.otherCollider.GetComponent<HP>().Heal(healing_points);
                heal_last_time = Time.time;
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
