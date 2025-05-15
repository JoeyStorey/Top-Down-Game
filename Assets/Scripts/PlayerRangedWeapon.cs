using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedWeapon : MonoBehaviour
{
    public int damage;
    public int projectileLifetime;
    private GameObject player;
    private PlayerCombat playerCombat;


    void Start()
    {
        player = GameObject.Find("Player");
        playerCombat = player.GetComponent<PlayerCombat>();

        Destroy(gameObject, projectileLifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon collided with " + other.tag);
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
