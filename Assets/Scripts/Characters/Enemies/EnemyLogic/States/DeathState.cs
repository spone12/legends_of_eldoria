using UnityEngine;

public class DeathState : IEnemyState {

    public void EnterState(EnemyAI enemyAI) {
        enemyAI.NavMeshAgent.ResetPath();
        enemyAI.NavMeshAgent.isStopped = true;
    }

    public void UpdateState(EnemyAI enemyAI) {
        // Do nothing
    }

    public void ExitState(EnemyAI enemyAI) {
        // Not applicable
    }
}
