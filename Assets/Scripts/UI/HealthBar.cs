using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private AliveObject _aliveObject;
    [SerializeField] private float _duration;
    [SerializeField] private Slider _slider;

    private Coroutine _changeHPJob;
    private RectTransform _rectTransform;

    private void OnEnable()
    {
        _aliveObject.HealthChanged += ChangeHPWork;
        _aliveObject.DirectionChanged += ChangeDirection;
    }

    private void OnDisable()
    {
        _aliveObject.HealthChanged -= ChangeHPWork;
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private IEnumerator ChangeHP(float target, float previousValue, float maxValue)
    {
        float elapsedTime = 0;
        float startValue = previousValue;
        float current = startValue;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            current = Mathf.Lerp(startValue, target, elapsedTime / _duration);
            _slider.value = current / (maxValue - 1);

            yield return null;
        }
    }

    private void ChangeHPWork(float target, float previousValue, float maxValue)
    {
        if (_changeHPJob != null)
        {
            StopCoroutine(_changeHPJob);
        }

        _changeHPJob = StartCoroutine(ChangeHP(target, previousValue, maxValue));
    }

    private void ChangeDirection(float sign)
    {
        _rectTransform.localScale = new Vector3(Mathf.Abs(_rectTransform.localScale.x) * sign, _rectTransform.localScale.y , _rectTransform.localScale.z);
    }
}
