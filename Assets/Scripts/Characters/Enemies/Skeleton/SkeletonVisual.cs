using UnityEngine;

[RequireComponent(typeof(Animator))] 
public class SkeletonVisual : MonoBehaviour
{
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private EnemyAI _enemyAI;

    private Animator _animator;
    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
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
}
