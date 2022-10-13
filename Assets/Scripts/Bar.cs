using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;

    private float _waitForSecondsInterval = 0.01f;
    private float _deltaHealth = 1f;
    private Coroutine _setTargetHealth;

    public void OnValueChanged(float value, float maxValue)
    {
        float targetValue = value / maxValue;
        SetTargetHealth(targetValue);
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChanged;
    }

    private IEnumerator ChangeHealthCorutine(float targetHealth)
    {
        var timeInterval = new WaitForSeconds(_waitForSecondsInterval);

        while (_slider.value != targetHealth)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetHealth, _deltaHealth * Time.deltaTime);

            yield return timeInterval;
        }
    }

    private void SetTargetHealth(float targetHealth)
    {
        if (_setTargetHealth != null)
            StopCoroutine(_setTargetHealth);

        _setTargetHealth = StartCoroutine(ChangeHealthCorutine(targetHealth));
    }
}
