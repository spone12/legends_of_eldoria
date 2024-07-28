using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instanse {  get; private set; }
    private Rigidbody2D rb;

    [SerializeField] private float movingSpeed = 12f;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake() {
        Instanse = this; // Into the Instanse property we write this class itself
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
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
