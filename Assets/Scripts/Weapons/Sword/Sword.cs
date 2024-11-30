using System;
using UnityEngine;

public class Sword : MonoBehaviour {

    [SerializeField] private int _damageAmount = 2;

    public event EventHandler OnSwordSwing;

    private PolygonCollider2D _polygonCollider2D;

    private void Awake() {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start() {
        AttackColliderTurnOff();
    }

    /**
     * Sword attack
     */
    public void Attack() {

        AttackColliderTurnOffOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    /**
     * Attack trigger when sword hits enemy
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(_damageAmount);
        }
    }

    /**
    * Attack collider OFF
    */
    public void AttackColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    }

    /**
     * Attack collider ON
     */
    private void AttackColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }

    /**
     * Attack collider off and on for animation correct
     */
    private void AttackColliderTurnOffOn() {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
}
