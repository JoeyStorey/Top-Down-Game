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
        if (invulnerable == true)
        {
            StartCoroutine(setInvuln());
        }
    }

    //Utter waste of time but I need a enumerator instead of a function to use wait for seconds :)
    IEnumerator setInvuln()
    {
        invulnerable = true;
        yield return new WaitForSecondsRealtime(1f);
        invulnerable = false;
    }

    public void CallInvuln()
    {
        StartCoroutine(setInvuln());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && invulnerable == false)
        {
            currentHealth = currentHealth - 20;
            Debug.Log("Ow!" + currentHealth);
            UIHealth.GetComponent<UIHealth>().UpdateHealth(currentHealth);
        }
    }
}
