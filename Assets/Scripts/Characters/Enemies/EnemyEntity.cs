using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private void Start() {
        _maxHealth = _currentHealth;
    }

    /**
     * Damage to the enemy
     */
    public void TakeDamage(int damage) {
        _currentHealth -= damage;
        DetectDeath();
    }

    /**
     * Enemy death check
     */
    public void DetectDeath() {

        if (_currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
