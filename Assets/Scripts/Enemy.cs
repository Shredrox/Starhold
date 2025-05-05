using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector]
    public float currentHealth;

    private readonly float speed = 0.1f;
    [HideInInspector]
    public Transform[] pathPoints;
    private int currentPointIndex = 0;

    private Base theBase;

    public event System.Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        theBase = Object.FindFirstObjectByType<Base>();
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
            float distance = Vector3.Distance(transform.position, targetPoint.position);

            float step = speed * Time.deltaTime;

            if (step > distance)
            {
                step = distance;
            }

            Vector3 direction = (targetPoint.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.0001f)
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
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    private void ReachBase()
    {
        if (theBase != null)
        {
            theBase.TakeDamage(10f);
        }
        Destroy(gameObject);
    }
}
