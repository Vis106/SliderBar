using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 50;

    private float _minHealth = 0;

    public event UnityAction<float, float> HealthChanged;

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {        
        _currentHealth = Mathf.Clamp(_currentHealth-damage, _minHealth, _maxHealth);       
        HealthChanged?.Invoke(Mathf.Clamp(_currentHealth, _minHealth, _maxHealth), _maxHealth);
    }

    public void TakeHeal(float heal)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + heal, _minHealth, _maxHealth);
        HealthChanged?.Invoke(Mathf.Clamp(_currentHealth, _minHealth, _maxHealth), _maxHealth);
    }
}
