using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 50;       

    public event UnityAction<float, float> HealthChanged;

    public void ChangeHealth(float deltaHealth)
    {
        float targetHealth = _currentHealth + deltaHealth;      
        _currentHealth += deltaHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public float GetStartHealth()
    {
        return _currentHealth / _maxHealth;
    }
}
