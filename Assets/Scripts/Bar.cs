using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;

    private float _waitForSecondsInterval = 0.01f;
    private float deltaHealth = 1f;
    private Coroutine _setTargetHealth;

    public void OnValueChanged(float value, float maxValue)
    {
        float targetValue = value / maxValue;
        SetTargetHealth(deltaHealth, targetValue, _waitForSecondsInterval);     
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnValueChanged;
        _slider.value = _player.GetStartHealth();
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChanged;
    }

    private IEnumerator ChangeHealthCorutine(float deltaHealth, float targetHealth, float waitForSecondsInterval)
    {
        var timeInterval = new WaitForSeconds(waitForSecondsInterval);
       
        Debug.LogError(targetHealth);

        while (_slider.value != targetHealth)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetHealth, deltaHealth * Time.deltaTime);
            Debug.Log(_slider.value);

            yield return timeInterval;
        }
    }

    private void SetTargetHealth(float deltaHealth, float targetHealth, float waitForSecondsInterval)
    {
        if (_setTargetHealth != null)
            StopCoroutine(_setTargetHealth);

        _setTargetHealth = StartCoroutine(ChangeHealthCorutine(deltaHealth, targetHealth, waitForSecondsInterval));
    }
}
