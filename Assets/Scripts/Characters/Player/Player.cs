using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instanse {  get; private set; }

    // Player moving speed
    [SerializeField] private float _movingSpeed = 12f;
    [SerializeField] private int _maxHealth = 30;
    [SerializeField] private float _damageRecoveryTime = 0.5f;

    // Player min moving speed
    private float _minMovingSpeed = 0.1f;

    // Player is running
    private bool _isRunning = false;
    private int _currentHealth;
    private bool _canTakeDamage;

    Vector2 inputVector;

    private KnockBack _knockBack;
    private Rigidbody2D _rb;

    private void Awake() {
        Instanse = this; // Into the Instanse property we write this class itself
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start() {
        _currentHealth = _maxHealth;
        _canTakeDamage = true;
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
    }

    /**
     * 
     */
    private void Update() {
        inputVector = GameInput.Instance.GetMovementVector();
    }

    /**
     *  Update is called once per frame
     */
    private void FixedUpdate() {


        if (_knockBack.IsKnockedBack) {
            return;
        }

        HandleMovement();
    }

    /**
     * Player is running
     */
    public bool IsRunning() { return _isRunning; }

    /**
     * Take damage to player
     */
    public void TakeDamage(Transform damageSource, int damage) {
        if (_canTakeDamage) {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth -= damage);
            Debug.Log(_currentHealth);
            _knockBack.GetKnockedBack(damageSource);

            StartCoroutine(DamageRecoveryCorutine());
        }

        DetectDamage();
    }

    /**
     * Get player position
     */
    public Vector3 GetPlayerScreenPostion() {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerPos;
    }

    private void DetectDamage() {
        if (_currentHealth == 0) {
            Destroy(this.gameObject);
        }
    }

    /**
     * Coroutine of delaying the summoning of enemy re-attacks
     */
    private IEnumerator DamageRecoveryCorutine() {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    /**
     * Trigger to track a character's attack
     */
    private void Player_OnPlayerAttack(object sender, EventArgs e) {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    /**
     *  Player handle movement
     */
    private void HandleMovement() {
        _rb.MovePosition(_rb.position + inputVector * (_movingSpeed * Time.fixedDeltaTime));

        // Running
        if (Mathf.Abs(inputVector.x) > _minMovingSpeed || Mathf.Abs(inputVector.y) > _minMovingSpeed) {
            _isRunning = true;
        } else {
            _isRunning = false;
        }
    }
}
