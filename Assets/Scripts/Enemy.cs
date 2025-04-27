using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    private Transform[] path;
    private int currentPointIndex = 0;

    public void SetPath(Transform[] points)
    {
        path = points;
    }

    private void Update()
    {
        if (path == null) return;

        transform.position = Vector3.MoveTowards(transform.position, path[currentPointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, path[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= path.Length)
            {
                Destroy(gameObject);
                // Damage player here if you want
            }
        }
    }

    public float maxHealth = 100f;
    [HideInInspector]
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
