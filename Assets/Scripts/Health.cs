using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float currentHealth;
    private float maxHealth;
    private float healthRegenRate;

    public Action<float> OnHealthUpdate;
    //capsulation

    public Health() { }

    public Health(float _maxHealth) {
        maxHealth = _maxHealth;
    }

    public Health(float _maxHealth, float _healthRegenRate, float _currentHealth = 100) {
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        healthRegenRate = _healthRegenRate;

        OnHealthUpdate?.Invoke(currentHealth);// ? check for null
    }


    public void SetHealth(float value) {
        if (value > maxHealth || value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        currentHealth = value;
    }

    public void RegenHealth() {
        AddHealth(healthRegenRate * Time.deltaTime);
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void AddHealth(float value) {
        currentHealth = Mathf.Min(maxHealth, currentHealth + value);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void DeductHealth(float value) {
        currentHealth = Mathf.Max(0, currentHealth - value);
        OnHealthUpdate?.Invoke(currentHealth);
    }

}
