using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instanse { get; private set; }

    [SerializeField] private int _maxHealth = 30;
    [SerializeField] private float _damageRecoveryTime = 0.5f;

    private KnockBack _knockBack;
    public event EventHandler OnPlayerDeath;

    private int _currentHealth;
    private bool _canTakeDamage = true;

    private void Awake() {
        Instanse = this;
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start() {
        _currentHealth = _maxHealth;
    }

    /**
     * Take damage to player
     */
    public void TakeDamage(Transform damageSource, int damage) {
        if (Player.Instanse.IsPlayerAlive() && _canTakeDamage) {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth -= damage);
            Debug.Log(_currentHealth);
            _knockBack.GetKnockedBack(damageSource);

            StartCoroutine(DamageRecoveryCorutine());
        }

        CheckIfPlayerDeath();
    }

    /**
     * Detect player death
     */
    private void CheckIfPlayerDeath() {
        if (Player.Instanse.IsPlayerAlive() && _currentHealth == 0) {
            Player.Instanse._isAlive = false;
            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    /**
     * Coroutine of delaying the summoning of enemy re-attacks
     */
    private IEnumerator DamageRecoveryCorutine() {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
}
