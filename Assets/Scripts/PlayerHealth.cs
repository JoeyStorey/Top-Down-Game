using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private bool invulnerable;
    public UIHealth UIHealth;

    // Start is called before the first frame update
    void Start()
    {
        invulnerable = true;
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    //These two functions are called when rolling to prevent damage being taken.
    public void CallInvuln()
    {
        invulnerable = true;
    }

    public void RemoveInvuln()
    {
        invulnerable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && invulnerable == false)
        {
            currentHealth = currentHealth - 20;
            //Debug.Log("Ow!" + currentHealth);
            UIHealth.GetComponent<UIHealth>().UpdateHealth(currentHealth);
        }
    }
}
