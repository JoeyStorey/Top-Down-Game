using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject weaponPosition;
    public GameObject sword;
    //Corresponds to if a player has each specific weapon or not, i.e [1,0,0] means the player only has the sword
    public bool[] currentInventory;
    public GameObject currentWeapon;
    public Animator animator;
    public PlayerMovement playerMovement;
    public bool midAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Sets the current weapon as the sword as the player starts with it and will always have it.
        currentWeapon = Instantiate(sword, weaponPosition.transform);
        playerMovement = GetComponent<PlayerMovement>();
        midAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        AttackCheck();
    }

    void AttackCheck()
    {
        if (Input.GetMouseButtonDown(0) && playerMovement.midRoll == false && midAttack == false)
        {
            //TODO: Switch statement for different weapons
            Debug.Log("Swinging sword!");
            StartCoroutine(SwingSword());
        }
    }

    IEnumerator SwingSword()
    {
        midAttack = true;
        animator.SetBool("startSwing", true);
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool("startSwing", false);
        midAttack = false;
    }
}
