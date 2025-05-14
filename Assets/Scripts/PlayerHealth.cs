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

    //This function is used to update whether the player can take damage or not.
    public void SetInvuln(bool state)
    {
        if (state == true)
        {
            invulnerable = true;
        } else {
            invulnerable = false;
        } 
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
