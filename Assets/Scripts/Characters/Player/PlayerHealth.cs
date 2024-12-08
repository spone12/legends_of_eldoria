using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instanse { get; private set; }

    [Header("Health")]
    
    [SerializeField] private int _maxHealth = 30;
    [SerializeField] private int _currentHealth;

    public event EventHandler OnPlayerDeath;

    private KnockBack _knockBack;
    private Slider _hearthSlider;
    private TMP_Text _hpValueText;

    private bool _canTakeDamage = true;

    private void Awake() {
        Instanse = this;
        _hearthSlider = GameObject.Find("HealthPointsSlider").GetComponent<Slider>();
        _hpValueText = GameObject.Find("HPBarValue").GetComponent<TMP_Text>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start() {
        _currentHealth = _maxHealth;
        UpdateHealthSlider();
    }

    /**
     * Take damage to player
     */
    public void TakeDamage(Transform damageSource, int damage, float damageRecoveryTime = 0.5f) {

        if (Player.Instanse.IsPlayerAlive() && _canTakeDamage) {

            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth -= damage);
            UpdateHealthSlider();

            _knockBack.GetKnockedBack(damageSource);
            StartCoroutine(DamageRecoveryCorutine(damageRecoveryTime));
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
     * Health slider update
     */
    private void UpdateHealthSlider() {
        _hearthSlider.maxValue = _maxHealth;
        _hearthSlider.value = _currentHealth;
        _hpValueText.text = $"{_currentHealth} / {_maxHealth}";
    }

    /**
     * Coroutine of delaying the summoning of enemy re-attacks
     */
    private IEnumerator DamageRecoveryCorutine(float damageRecoveryTime) {
        yield return new WaitForSeconds(damageRecoveryTime);
        _canTakeDamage = true;
    }
}
