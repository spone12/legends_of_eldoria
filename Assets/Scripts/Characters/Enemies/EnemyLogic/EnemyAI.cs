using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour {

    public static EnemyAI Instanse { get; private set; }

    [Header("General Settings")]

    [Tooltip("Starting state")]
    [SerializeField] private State _startingState;
    private IEnemyState _currentState;
    private NavMeshAgent _navMeshAgent;

    [Header("Roaming Settings")]

    [Tooltip("Player detection distance")]
    [SerializeField] private float _playerDetectionDistance = 5f;
    [Tooltip("Min distance roaming")]
    [SerializeField] private float _roamingDistanceMin = 3f;
    [Tooltip("Max distance roaming")]
    [SerializeField] private float _roamingDistanceMax = 7f;
    [Tooltip("Roaming speed")]
    [SerializeField] private float _roamingSpeed;

    [Header("Chasing Settings")]

    [Tooltip("Roaming speed")]
    [SerializeField] private float _chasingSpeedMultiplier = 2f;
    [Tooltip("Roaming speed")]
    [SerializeField] private float _chasingLostThreshold = 10f;

    [Header("Attacking Settings")]

    [Tooltip("Attacking distance")]
    [SerializeField] private float _attackingDistance = 2f;
    [Tooltip("Attacking rate")]
    [SerializeField] private float _attackingRate = 2f;

    public IEnemyState CurrentState => _currentState;
    public float PlayerDetectionDistance => _playerDetectionDistance;
    public float RoamingDistanceMin => _roamingDistanceMin;
    public float RoamingDistanceMax => _roamingDistanceMax;
    public float RoamingSpeed => _roamingSpeed;
    public float ChasingSpeedMultiplier => _chasingSpeedMultiplier;
    public float ChasingLostThreshold => _chasingLostThreshold;
    public float AttackingDistance => _attackingDistance;
    public float AttackingRate => _attackingRate;

    public event EventHandler OnEnemyAttack;
    public Transform PlayerTransform { get; private set; }
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _roamingSpeed = _navMeshAgent.speed;

        PlayerTransform = GameObject.FindWithTag("Player").transform;
        Instanse = this;
        SetState(_startingState);
    }

    private void Update() {
        _currentState?.UpdateState(this);
    }

    /**
     * Set state
     */
    public void SetState(State newState) {

        Debug.Log($"Changing state to: {newState}");
        _currentState?.ExitState(this);
        _currentState = EnemyStateFactory.CreateState(newState, this);

        if (_currentState == null) {
            Debug.LogError($"State {newState} could not be created by EnemyStateFactory.");
            return;
        }

        _currentState.EnterState(this);
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
     * Transitioning to the enemy's death state
     */
    public void SetDeathState() {
        SetState(State.Death);
        enabled = false;
    }

    public void TriggerAttack() {
        OnEnemyAttack?.Invoke(this, EventArgs.Empty);
    }
}

public enum State {
    Idle,
    Roaming,
    Chasing,
    Attacking,
    Death
}
