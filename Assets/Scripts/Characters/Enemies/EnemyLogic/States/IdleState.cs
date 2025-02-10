using UnityEngine;

public class IdleState : IEnemyState {

    public void EnterState(EnemyAI enemyAI) {

        // Stopping movement
        enemyAI.NavMeshAgent.ResetPath();
    }

    public void UpdateState(EnemyAI enemyAI) {

        if (enemyAI.IsRoaming) {
            enemyAI.SetState(State.Roaming);
        }
 
        float distanceToPlayer = Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTransform.position);
        // When a player is detected, the status changes to pursuit
        if (enemyAI.IsChasing && distanceToPlayer < enemyAI.PlayerDetectionDistance) {
            enemyAI.SetState(State.Chasing);
        }
    }

    public void ExitState(EnemyAI enemyAI) {
        // Enemy exited Idle state
    }
}
