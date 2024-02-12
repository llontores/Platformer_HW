using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealPotion : MonoBehaviour
{
    [SerializeField] private int _healValue;
    public static event UnityAction<int> Collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Collected?.Invoke(_healValue);
            Destroy(gameObject);
        }
    }
}
