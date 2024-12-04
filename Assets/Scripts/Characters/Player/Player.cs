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

    private KnockBack _knockBack;
    private Rigidbody2D _rb;

    // Player min moving speed
    private float _minMovingSpeed = 0.1f;

    // Player is running
    private bool _isRunning = false;
    public bool _isAlive = true;

    Vector2 inputVector;

    private void Awake() {

        Instanse = this; // Into the Instanse property we write this class itself
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start() {
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
    }

    /**
     * Update
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
     * Get player position
     */
    public Vector3 GetPlayerScreenPostion() {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerPos;
    }

    /**
     * Player is running
     */
    public bool IsRunning() => _isRunning;

    /**
     * Is player alive
     */
    public bool IsPlayerAlive() => _isAlive;

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
