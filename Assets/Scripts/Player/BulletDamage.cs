using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletDamage : MonoBehaviour
{

    
    

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if bullet touches an enemy...
        if (other.CompareTag("Enemy") && gameObject.CompareTag("Bullet"))
        {
            // finds PlayerShoots script which is on the Player GameObject
            PlayerShoots playerShoots = GameObject.Find("Player").GetComponent<PlayerShoots>();
            // finds the int Damage on the PlayerShoots script
            int damage = playerShoots.damage;

            /* Finds the script "Enemy" attached to another gameObject
            //they take damage as per their TakeDamage function in the
            //Enemy Script.
            The damage they take is equal to the damage int in the 
            PlayerShoots script */
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
