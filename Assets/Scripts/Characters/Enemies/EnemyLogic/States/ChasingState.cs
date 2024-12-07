using UnityEngine;

public class ChasingState : IEnemyState {

    public void EnterState(EnemyAI enemyAI) {
        enemyAI.NavMeshAgent.speed *= enemyAI.ChasingSpeedMultiplier;
    }

    public void UpdateState(EnemyAI enemyAI) {

        float distanceToPlayer = Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTransform.position);

        // Player too far, switching to Roaming
        if (distanceToPlayer > enemyAI.ChasingLostThreshold) {
            enemyAI.SetState(State.Roaming);
            return;
        }

        // Player close enough, switching to Attacking
        if (distanceToPlayer <= enemyAI.AttackingDistance) {
            enemyAI.SetState(State.Attacking);
            return;
        }

        enemyAI.NavMeshAgent.SetDestination(enemyAI.PlayerTransform.position);
        ChangeFacingDirection(enemyAI, enemyAI.transform.position, enemyAI.PlayerTransform.position);
    }

    public void ExitState(EnemyAI enemyAI) {
        enemyAI.NavMeshAgent.speed /= enemyAI.ChasingSpeedMultiplier;
    }

    /**
    * Enemy rotation
    */
    private void ChangeFacingDirection(EnemyAI enemyAI, Vector3 sourcePosition, Vector3 targetPosition) {
        if (sourcePosition.x > targetPosition.x) {
            enemyAI.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            enemyAI.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
