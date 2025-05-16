using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject weaponPosition;
    public GameObject sword;
    public GameObject shuriken;
    public GameObject dagger;

    //Corresponds to if a player has each specific weapon or not, i.e [1,0,0] means the player only has the sword
    private GameObject currentWeapon;
    private int currentWeaponNumber;
    public Animator animator;
    public PlayerMovement playerMovement;
    public bool midAttack;
    public float projectileSpeed;

    public UIEquipment UIEquipment;

    public enum Weapon
    {
        Sword = 0,
        Shuriken = 1,
        Dagger = 2
    }
    
    //Saves the weapons accessible to the player
    public HashSet<Weapon> Weapons { get; private set; } = new HashSet<Weapon> { Weapon.Sword };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Sets the current weapon as the sword as the player starts with it and will always have it.
        currentWeapon = Instantiate(sword, weaponPosition.transform);
        currentWeaponNumber = 0;
        playerMovement = GetComponent<PlayerMovement>();
        midAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        AttackCheck();
        SwitchCheck();
    }

    void AttackCheck()
    {
        if (Input.GetMouseButtonDown(0) && playerMovement.midRoll == false && midAttack == false)
        {
            switch (currentWeaponNumber)
            {
                case 0:
                    StartCoroutine(SwingSword());
                    break;
                case 1:
                    StartCoroutine(ThrowShuriken());
                    break;
                case 2:
                    StartCoroutine(ThrowDagger());
                    break;
                default:
                    break;
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupWeapon"))
        {
            //Gets the type of weapon just picked up and adds it to the player inventory
            Weapon weaponType; 
            Enum.TryParse(other.GetComponent<Pickups>().pickupType, out weaponType);
            Weapons.Add(weaponType);
            Debug.Log(weaponType + "acquired!");
        }
    }

    void SwitchCheck()
    {
        //Very rudimentary, might change later
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(Weapon.Sword);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2) && Weapons.Contains(Weapon.Shuriken))
        {
            SwitchWeapon(Weapon.Shuriken);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) && Weapons.Contains(Weapon.Dagger))
        {
            SwitchWeapon(Weapon.Dagger);
        }
    }

    void SwitchWeapon(Weapon newWeapon)
    {
        Destroy(currentWeapon);
        switch (newWeapon)
        {
            case Weapon.Sword:
                currentWeapon = Instantiate(sword, weaponPosition.transform);
                UIEquipment.UpdateEquipment("Sword");
                break;
            case Weapon.Shuriken:
                //Currently pointless for the ranged weapons
                currentWeapon = Instantiate(shuriken, weaponPosition.transform);
                UIEquipment.UpdateEquipment("Shuriken");
                break;
            case Weapon.Dagger:
                currentWeapon = Instantiate(dagger, weaponPosition.transform);
                UIEquipment.UpdateEquipment("Dagger");
                break;
        }
        currentWeaponNumber = (int)newWeapon;
    }

    IEnumerator SwingSword()
    {
        midAttack = true;
        animator.SetBool("startSwing", true);
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool("startSwing", false);
        midAttack = false;
    }

    IEnumerator ThrowShuriken()
    {
        midAttack = true;
        animator.SetBool("startSwing", true);

        //Creates a new shuriken and applies force in the direction the player is facing. It is rotated to face upwards.
        GameObject newProjectile = Instantiate(shuriken, weaponPosition.transform.position, Quaternion.Euler(90f, 0f, 90f));
        Rigidbody projectileRB = newProjectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(transform.forward * projectileSpeed);
        projectileRB.freezeRotation = true;
        yield return new WaitForSecondsRealtime(0.5f);

        animator.SetBool("startSwing", false);
        midAttack = false;
    }

    IEnumerator ThrowDagger()
    {
        //Essentially identical to the shuriken, except there is a bigger delay in swings and it deals more damage.
        midAttack = true;
        animator.SetBool("startSwing", true);

        //Rotation currently broken, only throws sideways.
        Quaternion newRotation = transform.rotation;
        newRotation.y += 90;
        GameObject newProjectile = Instantiate(dagger, weaponPosition.transform.position, transform.rotation);
        Rigidbody projectileRB = newProjectile.GetComponent<Rigidbody>();
        projectileRB.AddForce(transform.forward * projectileSpeed);
        projectileRB.freezeRotation = true;
        yield return new WaitForSecondsRealtime(1f);

        animator.SetBool("startSwing", false);
        midAttack = false;
    }
}
