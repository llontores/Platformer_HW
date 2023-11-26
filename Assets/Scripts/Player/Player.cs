using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _horizontalInput;
    private float _sign;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void ApplyDamage(int damage)
    {
        _animator.SetTrigger("TakeHit");
        _health -= damage;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(_horizontalInput) > 0.01)
        {
            _animator.SetBool("IsWalking", true);
            float sign = Mathf.Sign(_horizontalInput);
            transform.localScale = new Vector3(sign * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        _animator.SetBool("IsWalking", false);

        _rigidbody2D.velocity = new Vector2(_speed * _horizontalInput, _rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
            _rigidbody2D.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
    }


}
