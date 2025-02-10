using UnityEngine;

public class SkeletonVisual : EnemyVisual
{
    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";
    private const string TAKE_HIT = "TakeHit";
    private const string IS_DIE = "IsDie";

    private void Start() {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
        _enemyEntity.OnDeath += _enemyEntity_OnDeath;
    }

    private void OnDestroy() {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
    }

    private void Update() {
        _animator.SetBool(IS_RUNNING, _enemyAI.IsRunning());
        _animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
    }

    /**
     * Trigger attack animation off
     */
    public void TriggerAttackAnimationTurnOff() {
        _enemyEntity.PolygonColliderTurnOff();
    }

    /**
     * Trigger attack animation on
     */
    public void TriggerAttackAnimationTurnOn() {
        _enemyEntity.PolygonColliderTurnOn();
    }

    /**
     * Event enemy attack
     */
    private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e) {
        _animator.SetTrigger(ATTACK);
    }

    /**
     * Trigger event - take hit to skeleton
     */
    private void _enemyEntity_OnTakeHit(object sender, System.EventArgs e) {
        _animator.SetTrigger(TAKE_HIT);
    }

    /**
     * Trigger event - skeleton die
     */
    private void _enemyEntity_OnDeath(object sender, System.EventArgs e) {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
        _enemyShadow.SetActive(false);
    }
}
