using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AliveObject : MonoBehaviour
{
    [SerializeField] protected float _health;

    public event UnityAction<float, float, float> HealthChanged;
    protected event UnityAction<float> DirectionChanged;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;    
    }

    public virtual void ApplyDamage(int damage)
    {
        float previousHealthAmount = _health;
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        HealthChanged?.Invoke(_health, previousHealthAmount, _maxHealth);

    }
    public void ApplyAidKit(int healValue)
    {
        float previousHealthAmount = _health;
        _health = Mathf.Clamp(_health + healValue, 0, _maxHealth);  
        HealthChanged?.Invoke(_health, previousHealthAmount, _maxHealth);
    }

    protected void InvokeDirectionChangedEvent(float sign)
    {
        DirectionChanged?.Invoke(sign);
    }
}
