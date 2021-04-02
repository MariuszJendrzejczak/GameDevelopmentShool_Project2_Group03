using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    TextMeshPro healthText;
    Unit currentHealt;

    void Start()
    {
        healthText = GetComponent<TextMeshPro>();
        currentHealt = GetComponentInParent<Unit>();
    }

    void Update()
    {
        CurrentHealth();
    }

    void CurrentHealth()
    {
        healthText.text = "" + currentHealt.unitHealth;
    }
}
