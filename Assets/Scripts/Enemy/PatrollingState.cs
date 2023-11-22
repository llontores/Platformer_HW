using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PatrollingState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = _leftEdge.position;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _animator.Play("Walk"); 
    }

    private void Update()
    {
        if(transform.position.x <= _leftEdge.position.x)
        {
            _targetPosition = _rightEdge.position;
        }
        else if(transform.position.x >= _rightEdge.position.x) 
        {
            _targetPosition = _leftEdge.position;
        }

        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, _targetPosition.x, _speed * Time.deltaTime), transform.position.y, 0);
        _spriteRenderer.flipX = (transform.position.x == _leftEdge.position.x) ? true : (transform.position.x == _rightEdge.position.x) ? false : _spriteRenderer.flipX;
    }
}
