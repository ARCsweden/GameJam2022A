using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{

    private float healing_points = 1.0f;
    private float heal_last_time;
    private float heal_time_period = 2.0f;
    public List<GameObject> listOfObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (!listOfObjects.Contains(col.gameObject) && (col.CompareTag("Player") || col.CompareTag("Enemy")))
        {
            listOfObjects.Add(col.gameObject);
            //Debug.Log("Adding: " + col.name);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (listOfObjects.Contains(col.gameObject))
        {
            listOfObjects.Remove(col.gameObject);
            //Debug.Log("Removing: " + col.name);
        }
    }

    void Update()
    {
        if (listOfObjects.Count > 0)
        {
            foreach (GameObject obj in listOfObjects)
            {
                if (Time.time - heal_last_time > heal_time_period)
                {
                    try
                    {
                        obj.GetComponent<HP>().Heal(healing_points);
                        heal_last_time = Time.time;
                        Debug.Log("Heal: " + obj.name);
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
