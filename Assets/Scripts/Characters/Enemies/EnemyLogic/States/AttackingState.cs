using System;
using UnityEngine;

public class AttackingState : IEnemyState {

    private float _nextAttackTime;

    public void EnterState(EnemyAI enemyAI) {
        if (!enemyAI.IsAttacking) {
            enemyAI.SetState(State.Roaming);
        }
        _nextAttackTime = Time.time;
    }

    public void UpdateState(EnemyAI enemyAI) {

        // Enemy attacks
        if (Time.time >= _nextAttackTime) {
            enemyAI.TriggerAttack();
            _nextAttackTime = Time.time + enemyAI.AttackingRate;
        }

        // If the player has moved out of the enemy's attack range, we start pursuing the hero
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTransform.position) > enemyAI.AttackingDistance) {
            enemyAI.SetState(State.Chasing);
        }
    }

    public void ExitState(EnemyAI enemyAI) {
        // Cleanup
    }
}
