using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{

    private float damage = 1.0f;
    private float fire_last_time;
    private float fire_time_period = 2.0f;
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
    // Update is called once per frame
    void Update()
    {
        if (listOfObjects.Count > 0)
        {
            foreach (GameObject obj in listOfObjects)
            {
                if (Time.time - fire_last_time > fire_time_period)
                {
                    try
                    {
                        Debug.Log("Fire damage: " + obj.name);
                        obj.GetComponent<HP>().Damage(damage);
                        fire_last_time = Time.time;
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
