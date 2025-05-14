using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int damage;
    private GameObject player;
    private PlayerCombat playerCombat;

    void Start()
    {
        player = GameObject.Find("Player");
        playerCombat = player.GetComponent<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword collided with " + other.tag);
        if (other.CompareTag("Enemy") && playerCombat.midAttack == true)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.ReceiveDamage(damage);
        }
    }
}
