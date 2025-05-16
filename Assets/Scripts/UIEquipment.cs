using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipment : MonoBehaviour
{
    public Text textField;

    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<Text>();
        textField.text = "Current Weapon: Sword";
    }

    public void UpdateEquipment(string weaponName)
    {
        textField.text = "Current Weapon: " + weaponName; 
    }
}
