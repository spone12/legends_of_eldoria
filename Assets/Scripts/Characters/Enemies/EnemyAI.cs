using UnityEngine;
using UnityEngine.AI;
using Project.Utils;
using System;

public class EnemyAI : MonoBehaviour {

    private NavMeshAgent _navMeshAgent;

    private State _currentState;
    [SerializeField] private State _startingState;

    private float _roamingTimer;
    private Vector3 _roamPosition;
    private Vector3 _startingPostion;
    [SerializeField] private float _roamingDistanceMin = 3f; // Min distance
    [SerializeField] private float _roamingDistanceMax = 7f; // Max distance
    [SerializeField] private float _roamingTimerMax = 2f; // Movement time

    [SerializeField] private bool _isAttackingEnemy = false;
    [SerializeField] private float _attackingDistance = 2f;
    [SerializeField] private float _attackRate = 2f;
    private float _nextAttackTime = 0f;

    [SerializeField] private bool  _isChaisingEnemy = false;
    [SerializeField] private float _chasingDistance = 4f;
    [SerializeField] private float _chasingSpeedMultiplier = 2f;
    private float _chaisingSpeed;

    private float _roamingSpeed;

    public event EventHandler OnEnemyAttack;

    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    public EnemyAI(State state) {
        _currentState = state;
    }

    // State machine
    public enum State {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death
    }

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;

        _roamingSpeed = _navMeshAgent.speed;
        _chaisingSpeed = _navMeshAgent.speed * _chasingSpeedMultiplier;
    }

    private void Start() {
        _startingPostion = transform.position;
    }

    private void Update() {
        StateHandler();
        MovementDirectionHandler();
    }

    /**
     * Current enemy state
     */
    private void StateHandler() {
        // current state
        switch (_currentState) {

            case State.Roaming:
                _roamingTimer -= Time.deltaTime;

                if (_roamingTimer < 0) {
                    Roaming(true);
                    _roamingTimer = _roamingDistanceMax;
                }
                CheckCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }
    }

    /**
    * Check current state
    */
    private void CheckCurrentState() {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instanse.transform.position);
        State newState = State.Roaming;

        // If chaising enemy
        if (_isChaisingEnemy) {
            if (distanceToPlayer <= _chasingDistance) {
                newState = State.Chasing;
            }
        }

        if (_isAttackingEnemy) {
            if (distanceToPlayer <= _attackingDistance) {
                newState = State.Attacking;
            }
        }

        // Reset navmesh path
        if (newState != _currentState) {
            if (newState == State.Chasing) {
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = _chaisingSpeed;
            } else if (newState == State.Roaming) {
                _roamingTimer = 0f;
                _navMeshAgent.speed = _roamingSpeed;
            } else if (newState == State.Attacking) {
                _navMeshAgent.ResetPath();
            }

            _currentState = newState;
        }
    }

    /**
     * Handling the enemy's turn to the hero
     */
    private void MovementDirectionHandler() {
        if (Time.time > _nextCheckDirectionTime) {
            if (IsRunning()) {
                ChangeFacingDirection(_lastPosition, transform.position);
            } else if (_currentState == State.Attacking) {
                // If the enemy has moved to the attack state, you must turn to the hero
                ChangeFacingDirection(transform.position, Player.Instanse.transform.position);
            }

            // Updating the new position
            _lastPosition = transform.position;
            // When's the next inspection
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }

    /**
     * Chaising target
     */
    private void ChasingTarget() {
        _navMeshAgent.SetDestination(Player.Instanse.transform.position);   
    }

    /**
     * Enemy attack
     */
    private void AttackingTarget() {
        // If the current time is longer than the next attack
        if (Time.time > _nextAttackTime) {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            _nextAttackTime = Time.time + _attackRate;
        }
    }

    /**
     * If enemy is running 
     */
    public bool IsRunning() {
        if (_navMeshAgent.velocity == Vector3.zero) {
            return false;
        }

        return true;
    }

    /**
     * Get roaming animation speed
     */
    public float GetRoamingAnimationSpeed() {
        return _navMeshAgent.speed / _roamingSpeed;
    }

    /**
     * Set new destination
     */
    private void Roaming(bool enemyWalkAnywhere = false) {

        _startingPostion = transform.position;
        _roamPosition = GetRoamingPosition();
        _navMeshAgent.SetDestination(_roamPosition);
    }
 
    /**
     * Finding a new path
     */
    private Vector3 GetRoamingPosition() {
        return _startingPostion + Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }

    /**
     * Enemy rotation
     */
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) {
        if (sourcePosition.x > targetPosition.x) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
