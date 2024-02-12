using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : AliveObject
{
    private const string AnimationAttackTrigger = "Attack";
    private const string AnimationTakingHitTrigger = "TakeHit";
    private const string AnimationWalkingSpeed = "Speed";
    private const string AnimationJumpningBool = "IsJumping";

    //[SerializeField] private int _health;
    [SerializeField] private LayerMask _enemiesMask;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _isGrounded;
    private float _horizontalInput;

    private void Start()
    {
        _isGrounded = true;
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        HealPotion.Collected += ApplyAidKit;
    }

    private void OnDisable()
    {
        HealPotion.Collected -= ApplyAidKit;
    }

    public override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        _animator.SetTrigger(AnimationTakingHitTrigger);
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(_horizontalInput) > 0.01)
        {
            _animator.SetFloat(AnimationWalkingSpeed, Mathf.Abs(_horizontalInput));
            float sign = Mathf.Sign(_horizontalInput);
            transform.localScale = new Vector3(sign * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            InvokeDirectionChangedEvent(sign);
        }

        _rigidbody2D.velocity = new Vector2(_speed * _horizontalInput, _rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            _animator.SetBool(AnimationJumpningBool, true);
            _rigidbody2D.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            _animator.SetBool(AnimationJumpningBool, false);
            _isGrounded = true;
        }
    }

    private void Attack()
    {
        _animator.SetTrigger(AnimationAttackTrigger);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemiesMask);

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if(_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
