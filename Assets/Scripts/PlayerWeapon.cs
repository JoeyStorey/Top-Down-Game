using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword collided with " + other.tag);
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.ReceiveDamage(damage);
        }
    }
}
