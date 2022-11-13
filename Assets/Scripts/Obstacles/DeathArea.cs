using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private float damage = 1.0f;
    private float damage_last_time;
    private float damage_time_period = 2.0f;
    public List<Collider> listOfObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (!listOfObjects.Contains(col) && (col.CompareTag("Player") || col.CompareTag("Enemy")))
        {
            listOfObjects.Add(col);
            //Debug.Log("Adding: " + col.name);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (listOfObjects.Contains(col))
        {
            listOfObjects.Remove(col);
            //Debug.Log("Removing: " + col.name);
        }
    }

    void Update()
    {
        if (listOfObjects.Count > 0)
        {
            foreach (Collider obj in listOfObjects)
            {
                if (Time.time - damage_last_time > damage_time_period)
                {
                    try
                    {
                        Debug.Log("Death damage: " + obj.name);
                        obj.GetComponent<HP>().Damage(damage);
                        damage_last_time = Time.time;
                    }
                    catch (System.Exception)
                    {
                        //throw;
                    }
                }
            }
        }
    }
}
