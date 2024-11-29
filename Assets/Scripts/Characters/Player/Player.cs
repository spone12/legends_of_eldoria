using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instanse {  get; private set; }
    private Rigidbody2D rb;

    // Player moving speed
    [SerializeField] private float movingSpeed = 12f;

    // Player min moving speed
    private float minMovingSpeed = 0.1f;

    // Player is running
    private bool isRunning = false;

    Vector2 inputVector;

    private void Awake() {
        Instanse = this; // Into the Instanse property we write this class itself
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        inputVector = GameInput.Instance.GetMovementVector();
    }
    // Update is called once per frame
    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        // Running
        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed) {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    public bool IsRunning() { return isRunning; }

    public Vector3 GetPlayerScreenPostion() {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerPos;
    }
}
