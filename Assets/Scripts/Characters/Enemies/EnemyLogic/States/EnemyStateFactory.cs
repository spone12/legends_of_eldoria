using UnityEngine;
using Unity.IO.LowLevel.Unsafe;

public static class EnemyStateFactory {
    public static IEnemyState CreateState(State state, EnemyAI enemyAI) {

        switch (state) {
            case State.Roaming: return new RoamingState();
            case State.Chasing: return new ChasingState();
            case State.Attacking: return new AttackingState();
            case State.Death: return new DeathState();
            case State.Idle: return new IdleState();
            default:
                Debug.LogError($"Unknown state: {state}");
                return null;
        }
    }
}