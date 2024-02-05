using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticedPlayer : Transition
{
    [SerializeField] private float _visibillityLength;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) <= _visibillityLength)
        {
            NeedTransit = true;
        }
    }
}
