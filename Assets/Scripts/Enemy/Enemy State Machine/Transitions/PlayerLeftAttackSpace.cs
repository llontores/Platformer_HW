using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftAttackSpace : Transition
{
    [SerializeField] private float _spaceToAttack;
    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) >= _spaceToAttack)
            NeedTransit = true;
    }
}
