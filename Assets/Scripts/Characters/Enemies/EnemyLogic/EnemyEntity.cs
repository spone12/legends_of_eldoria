using System;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private SkeletonDataSO _data;
    [SerializeField] private int _currentHealth;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    private void Awake() {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start() {
        _currentHealth = _data.Health;
    }

    /**
     * Damage to the enemy
     */
    public void TakeDamage(int damage) {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    /**
     * Disable polygon collider
    */
    public void PolygonColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    }

    /**
     * Enable polygon collider
     */
    public void PolygonColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }

    /**
     * Damage to player on attack
     */
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out Player player)) {
            PlayerHealth.Instanse.TakeDamage(transform, _data.AttackDamage);
        }
    }

    /**
     * Enemy death check
     */
    private void DetectDeath() {

        if (_currentHealth <= 0) {

            // Disabling the collision and impact hit box on the skeleton
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;

            _enemyAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
