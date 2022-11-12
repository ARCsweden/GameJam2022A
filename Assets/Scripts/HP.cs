using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float HealthPoints = 1;
    public float Armor = 0.75f;
    public float MaxHealthPoints = 20;
    private float time_of_last_heal;
    private float heal_period_time = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HealthPoints <= 0)
        {
            Death();
        }
    }

    public void Damage(float damage)
    {
        HealthPoints = HealthPoints - damage * (1 - Armor);

    }

    public void Heal(float heal)
    {
        if (Time.time - time_of_last_heal > heal_period_time)
        {
            if (HealthPoints + heal <= MaxHealthPoints)
            {
                HealthPoints = HealthPoints + heal;
                Debug.Log("Healing");
            }
            else
            {
                HealthPoints = MaxHealthPoints;
            }
            time_of_last_heal = Time.time;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
