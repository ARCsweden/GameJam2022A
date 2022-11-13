using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet;

    public float baseFiringSpeed = 1f;
    public float levelFiringSpeed = 1f;

    public float baseSpread = 0f;
    public float levelSpread = 0f;

    public int baseAmount = 1;
    public float levelAmount = 1f;

    // Start is called before the first frame update
    public void BANG()
    {
        int level = GetComponentInParent<Player>().Level;

        float spread = baseSpread + levelSpread * level;
        int amount = (int)(baseAmount + levelAmount * level);

        try
        {
            for(int i = 1; i <= amount; i++)
            {
                Instantiate(bullet, gameObject.transform.position + gameObject.transform.forward, transform.rotation * new Quaternion(0, Random.Range(0, spread), 0, 0), transform);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to brappbrapp");
            //throw;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.SetParent(other.transform);
            transform.position = other.transform.position + new Vector3(0.5f, 1, 0);
            other.GetComponent<Player>().attachedWeapon = gameObject;

            Destroy(GetComponent<SphereCollider>(), 0f);

        }
    }

}
