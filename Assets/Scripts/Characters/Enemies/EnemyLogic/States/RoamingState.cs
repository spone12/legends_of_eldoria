using UnityEngine;
using Project.Utils;

public class RoamingState : IEnemyState {
    private Vector3 _targetPosition;
    private float _roamingTimer;

    public void EnterState(EnemyAI enemyAI) {
        if (!enemyAI.IsRoaming) {
            enemyAI.SetState(State.Idle);
        }
        _roamingTimer = Random.Range(2f, 5f);
        SetRandomTarget(enemyAI);
    }

    public void UpdateState(EnemyAI enemyAI) {

        _roamingTimer -= Time.deltaTime;
        if (_roamingTimer <= 0) {
            SetRandomTarget(enemyAI);
            _roamingTimer = Random.Range(2f, 5f);
            Debug.Log(_roamingTimer);
        }

        // Player too close, switching to Chasing
        if (enemyAI.IsChasing && Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTransform.position) < enemyAI.PlayerDetectionDistance) {
            enemyAI.SetState(State.Chasing);
        }
    }

    public void ExitState(EnemyAI enemyAI) {
        enemyAI.NavMeshAgent.ResetPath();
    }

    /**
     * Set roaming destination random position
     */
    private void SetRandomTarget(EnemyAI enemyAI) {
        
        _targetPosition = enemyAI.transform.position + Utils.GetRandomDir() * Random.Range(enemyAI.RoamingDistanceMin, enemyAI.RoamingDistanceMax);
        enemyAI.NavMeshAgent.SetDestination(_targetPosition);

        ChangeFacingDirection(enemyAI, enemyAI.transform.position, _targetPosition);
    }

    /**
    * Enemy sprite rotation depending on direction
    */
    private void ChangeFacingDirection(EnemyAI enemyAI, Vector3 sourcePosition, Vector3 targetPosition) {
        if (sourcePosition.x > targetPosition.x) {
            enemyAI.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            enemyAI.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
