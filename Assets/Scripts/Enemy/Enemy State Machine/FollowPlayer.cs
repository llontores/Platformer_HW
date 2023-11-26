using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : State
{
    [SerializeField] private float _followDistance;
    [SerializeField] private float _followSpeed;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _renderer.flipX = (transform.position.x < Target.transform.position.x) ? true : (transform.position.x > Target.transform.position.x) ? false : _renderer.flipX;
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, Target.transform.position.x, _followSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
