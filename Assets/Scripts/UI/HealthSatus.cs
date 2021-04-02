using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthStatus : MonoBehaviour
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
