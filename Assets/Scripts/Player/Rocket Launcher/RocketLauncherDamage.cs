using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherDamage : MonoBehaviour
{

    public CircleCollider2D explosionCollider;

    // Start is called before the first frame update
    void Start()
    {
        explosionCollider.enabled = false;
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
            int damage = playerShoots.rocketLauncherDamage;

            /* Finds the script "Enemy" attached to another gameObject
            //they take damage as per their TakeDamage function in the
            //Enemy Script.
            The damage they take is equal to the damage int in the 
            PlayerShoots script */
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            /*
             * instead of destroy the gameObject (making the explosion collider disappear)
             * we want to change the sprite to the explosion animation
             * and turn on the explosion collider at the same time
             * and when the animation ends then destroy the object
             */
            Destroy(gameObject);
        }
    }
}
