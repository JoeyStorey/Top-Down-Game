using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float projectileLifetime;

    void Start()
    {
        Destroy(gameObject, projectileLifetime);
    }
}
