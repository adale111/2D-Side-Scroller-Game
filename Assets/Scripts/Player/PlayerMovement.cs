using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public int playerHealth;
    public float invincibilityDuration = 2f;
    public Renderer PlayerRenderer;
    private float lastDamageTime;
    public TextMeshProUGUI HealthText;


    void Start()
    {

        playerHealth = 20;
        HealthText.text = "Health: " + playerHealth;


    }
    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        // limit player's position to the screen area
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);




    }

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerShoots playerShoots = GetComponent<PlayerShoots>();
        
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") && Time.time - lastDamageTime > invincibilityDuration)
        {

            if (playerShoots.shieldCharged == true)
            {
                playerShoots.shieldCharged = false;
                playerShoots.shieldHit = true;
                TakeDamage(0);
            }
            else
            {
                TakeDamage(5);
            }
        }

      
    }

    public void TakeDamage(int damage)
    {
        lastDamageTime = Time.time;
        playerHealth -= damage;
        HealthText.text = ("Health: " + playerHealth);
        StartCoroutine(Flash());

        if (playerHealth <= 0)

        {
            PlayerDestroyed();
        }
    }

    IEnumerator Flash()
    {
        while (Time.time - lastDamageTime <= invincibilityDuration)
        {
            PlayerRenderer.enabled = !PlayerRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        PlayerRenderer.enabled = true;
    }

    void PlayerDestroyed()
    {
        Destroy(gameObject);
    }



}

