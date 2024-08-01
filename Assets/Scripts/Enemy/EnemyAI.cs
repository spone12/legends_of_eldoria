using System.Collections;
using System.Collections.Generic;
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
    private Vector3 roamPostion;
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
                    Roaming();
                    roamingTime = roamingDistanceMax;
                }
                break;
        }
    }

    // Set new destination
    private void Roaming() {
        roamPostion = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPostion);
    }

    // Finding a new path
    private Vector3 GetRoamingPosition() {
        return startingPostion + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
