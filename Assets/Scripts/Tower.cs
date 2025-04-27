using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRadius = 5f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    private Transform target;

    void Update()
    {
        FindTarget();

        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Fire();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, LayerMask.GetMask("Enemy"));

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hit.transform;
            }
        }

        target = closestEnemy;
    }

    void Fire()
    {
        if (bulletPrefab != null && target != null)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
        }
    }
}
