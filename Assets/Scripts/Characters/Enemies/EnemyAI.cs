using UnityEngine;
using UnityEngine.AI;
using Project.Utils;

public class EnemyAI : MonoBehaviour {

    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMin = 3f; // Min distance
    [SerializeField] private float roamingDistanceMax = 7f; // Max distance
    [SerializeField] private float roamingTimerMax = 2f; // Movement time

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPostion;

    // State machine
    private enum State {
        Idle,
        Roaming
    }

    private void Start() {
        startingPostion = transform.position;
    }

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    private void Update() {

        // current state
        switch (state) {
            default:
            case State.Idle:
                break;
            case State.Roaming:
                roamingTime -= Time.deltaTime;

                if (roamingTime < 0) {
                    Roaming(true);
                    roamingTime = roamingDistanceMax;
                }
                break;
        }
    }
 
    /**
     * Set new destination
     */
    private void Roaming(bool enemyWalkAnywhere = false) {

        if (enemyWalkAnywhere) {
            startingPostion = transform.position;
        }
        
        roamPosition = GetRoamingPosition();
        ChangeFacingDirection(startingPostion, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
    }
 
    /**
     * Finding a new path
     */
    private Vector3 GetRoamingPosition() {
        return startingPostion + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
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
