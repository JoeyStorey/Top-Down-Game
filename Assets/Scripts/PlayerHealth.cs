using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private bool invulnerable;
    public float invulnDuration;
    public UIHealth UIHealth;

    // Start is called before the first frame update
    void Start()
    {
        invulnerable = false;
        currentHealth = maxHealth;
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
        if ((other.CompareTag("Enemy") || other.CompareTag("EnemyProj") )&& invulnerable == false)
        {
            currentHealth -= 20;
            //Game Over
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(4);
            }
            UIHealth.GetComponent<UIHealth>().UpdateHealth(currentHealth);
            StartCoroutine(HitInvulnerability());
        }

        if (other.CompareTag("PickupHealth"))
        {
            Debug.Log("Yum! +" + currentHealth);
            currentHealth += 50;
            if (currentHealth > maxHealth) 
            {
                currentHealth = maxHealth;
            }
            UIHealth.GetComponent<UIHealth>().UpdateHealth(currentHealth);
        }
    }

    //Sets the player as invulnerable after taking damage
    IEnumerator HitInvulnerability()
    {
        SetInvuln(true);
        yield return new WaitForSecondsRealtime(invulnDuration);
        SetInvuln(false);
    }

}
