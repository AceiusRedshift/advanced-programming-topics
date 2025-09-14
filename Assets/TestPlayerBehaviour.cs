using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerBehaviour : MonoBehaviour
{
    private const float kMoveForce = 25;
    private const float kJumpForce = 50;
    private const float kGroundedRadius = .2f;

    private InputAction movementAction;
    private InputAction jumpAction;

    private Rigidbody2D rigidBody;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 wishVector = movementAction.ReadValue<Vector2>();

        rigidBody.linearVelocity += wishVector * (kMoveForce * Time.deltaTime);

        if (jumpAction.triggered && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * (kJumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, kGroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                // if (!wasGrounded)
                //     OnLandEvent.Invoke();
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("rigidBody.linearVelocity = " + rigidBody.linearVelocity);
        GUILayout.Label("isGrounded = " + isGrounded);
    }
}