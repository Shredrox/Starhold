using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector]
    public float currentHealth;

    public float speed = 2f;
    public Transform[] pathPoints; // Set by GameManager
    private int currentPointIndex = 0;

    private Base theBase; // reference to the base

    private void Start()
    {
        currentHealth = maxHealth;
        theBase = Object.FindFirstObjectByType<Base>(); // find the Base script in scene
    }

    private void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (currentPointIndex < pathPoints.Length)
        {
            Transform targetPoint = pathPoints[currentPointIndex];
            Vector3 dir = targetPoint.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
            {
                currentPointIndex++;
                if (currentPointIndex >= pathPoints.Length)
                {
                    ReachBase();
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Base Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void ReachBase()
    {
        if (theBase != null)
        {
            theBase.TakeDamage(10f); // damage the base
        }
        Destroy(gameObject); // destroy self
    }
}
