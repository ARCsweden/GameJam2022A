using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float HealthPoints = 1;
    public float Armor = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthPoints <= 0)
        {
            Death();
        }
    }

    public void Damage(float damage)
    {
        HealthPoints = HealthPoints - damage * (1 - Armor);

    }

    public void Death()
    {
        Destroy(this.GetComponent<GameObject>());
    }
}
