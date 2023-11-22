using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : State
{
    [SerializeField] private float _followDistance;
    [SerializeField] private float _followSpeed;

    private void Update()
    {
        while(Vector2.Distance(transform.position, Target.transform.position) > _followDistance)
        {
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, Target.transform.position.x, _followSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
}
