using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHoming : MonoBehaviour
{
    public float speed = 15f;
    public float detectionDistance = 25f;

    private GameObject closestEnemy;

    void Update()
    {
        if (closestEnemy == null || Vector2.Distance(transform.position, closestEnemy.transform.position) > detectionDistance)
        {
            FindClosestEnemy();
        }
        else
        {
            Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Predict the enemy's position and move towards it
            Rigidbody2D targetRigidbody = closestEnemy.GetComponent<Rigidbody2D>();
            Vector3 predictedTargetPosition = closestEnemy.transform.position + Vector3.Scale(targetRigidbody.velocity, Vector3.right + Vector3.up) * Time.deltaTime;
            direction = (predictedTargetPosition - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, predictedTargetPosition, speed * Time.deltaTime);

        }
    }

    void FindClosestEnemy()
    {
        // Find all game objects in the scene with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Find the closest enemy game object
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= detectionDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
    }
}