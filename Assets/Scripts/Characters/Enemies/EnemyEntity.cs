using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private PolygonCollider2D _polygonCollider2D;

    private void Awake() {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start() {
        _maxHealth = _currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Attack");
    }

    /**
     * Damage to the enemy
     */
    public void TakeDamage(int damage) {
        _currentHealth -= damage;
        DetectDeath();
    }

    /**
     * Disable polygon collidern
    */
    public void PolygonColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    }

    /**
     * Enable polygon collidern
     */
    public void PolygonColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }

    /**
     * Enemy death check
     */
    private void DetectDeath() {

        if (_currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
