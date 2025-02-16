public interface IEnemyState {
    void EnterState(EnemyAI enemyAI);
    void UpdateState(EnemyAI enemyAI);
    void ExitState(EnemyAI enemyAI);
}
