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

                if(gameObject.name.Contains("Machine"))
                {
                    Instantiate(bullet, transform.position + transform.forward, transform.rotation).GetComponent<GunBullet>().level = level;
                }
                else if (gameObject.name.Contains("Shotgun"))
                {
                    Instantiate(bullet, transform.position + transform.forward, transform.rotation).GetComponent<GunBullet>().level = level;
                }
                if (gameObject.name.Contains("Grenade"))
                {
                    Instantiate(bullet, transform.position + transform.forward, transform.rotation).GetComponent<GrenadeBullet>().level = level;
                }

            }
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to brappbrapp");
            //throw;
        }
    }

    public void RemoveTrigger()
    {
        Destroy(GetComponent<SphereCollider>(), 0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(other.GetComponent<Player>().attachedWeapon, 0f);

            transform.SetParent(other.transform);
            transform.position = other.transform.position + new Vector3(0.5f, 0.5f, 0);
            other.GetComponent<Player>().attachedWeapon = gameObject;

            Destroy(GetComponent<SphereCollider>(), 0f);

        }
    }

}
