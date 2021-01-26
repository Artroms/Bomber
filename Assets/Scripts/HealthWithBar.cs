using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthWithBar : Health
{
    [SerializeField] private HealthBar healthBar;
    protected virtual void Start()
    {
        healthBar = Instantiate(healthBar);
        healthBar.target = transform;
        healthBar.maxValue = hp;
        onDamage.AddListener(healthBar.SetHealthInfo);
    }

    private void OnDisable()
    {
        healthBar?.gameObject.SetActive(false);
    }
}
