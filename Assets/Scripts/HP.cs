using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public GameObject ThisGameObject;

    public float MaxHealthPoints = 20;
    public float HealthPoints = 1;
    public float Armor = 0.75f;

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
        if (HealthPoints + heal > MaxHealthPoints){
            HealthPoints = MaxHealthPoints;
        }
        else {
            HealthPoints = HealthPoints + heal;
        }
    }

    public void Death()
    {


        if (ThisGameObject.tag == "Player")
        {
            //ThisGameObject.SetActive(false);
            ThisGameObject.GetComponent<Player>().Alive = false;
            ThisGameObject.GetComponent<CharacterController>().enabled = false;
            ThisGameObject.GetComponent<Player>().enabled = false;
            ThisGameObject.transform.position = new Vector3(0, 104, 0);
        }
        else if (ThisGameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
