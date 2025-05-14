using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;

    public void ReceiveDamage(int damage)
    {
        health = health - damage;
        Debug.Log("Enemy Health:" + health);
    }
}
