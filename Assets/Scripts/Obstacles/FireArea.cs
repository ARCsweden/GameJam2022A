using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{

    private float damage = 1.0f;
    private float fire_last_time;
    private float fire_time_period = 2.0f;
    public List<Collider> listOfObjects;
    // Start is called before the first frame update
    void Start()
    {

    }

    /*     void OnTriggerStay(Collider collision)
        {
            if (Time.time - fire_last_time > fire_time_period)
            {
                try
                {
                    collision.gameObject.GetComponent<HP>().Damage(damage);
                    fire_last_time = Time.time;
                    //Debug.Log("Fire: " + collision.gameObject.name);
                }
                catch (System.Exception)
                {
                    //throw;
                }
            }
        } */

    void OnTriggerEnter(Collider col)
    {
        if (!listOfObjects.Contains(col) && (col.CompareTag("Player") || col.CompareTag("Enemy")))
        {
            listOfObjects.Add(col);
            Debug.Log("Adding: " + col.name);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (listOfObjects.Contains(col))
        {
            listOfObjects.Remove(col);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (listOfObjects.Count > 0)
        {
            foreach (Collider obj in listOfObjects)
            {
                if (Time.time - fire_last_time > fire_time_period)
                {
                    try
                    {
                        obj.gameObject.GetComponent<HP>().Damage(damage);
                        fire_last_time = Time.time;
                        //Debug.Log("Fire: " + collision.gameObject.name);
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
