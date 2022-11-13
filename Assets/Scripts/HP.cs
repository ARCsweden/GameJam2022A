using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float HealthPoints = 1;
    public float Armor = 0.75f;
    public float MaxHealthPoints = 20;

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

    /* public void Damage(float damage)
    {
        HealthPoints = HealthPoints - damage * (1 - Armor);

    } */

    public void Damage(float damage)
    {
        HealthPoints = HealthPoints - damage * (1 - Armor);
        Debug.Log("Damaging: " + gameObject.name);
    }

    public void Heal(float heal)
    {
        if (HealthPoints + heal <= MaxHealthPoints)
        {
            HealthPoints = HealthPoints + heal;
            Debug.Log("Healing: " + gameObject.name);
        }
        else
        {
            HealthPoints = MaxHealthPoints;
        }
    }

    public void Death()
    {
        Debug.Log("Died: " + gameObject.name);
        Destroy(gameObject);
    }
}
