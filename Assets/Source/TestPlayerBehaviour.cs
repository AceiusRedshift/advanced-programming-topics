using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerBehaviour : MonoBehaviour
{
    private const float kMoveForce = 25;
    private const float kJumpForce = 500;
    private const float kGroundedRadius = .2f;

    private InputAction movementAction;
    private InputAction jumpAction;

    private Rigidbody2D rigidBody;
    private Collider2D hitbox;
    
    private int groundLayerMask;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        movementAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rigidBody = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();
        
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    private void Update()
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
        isGrounded = Physics2D.IsTouchingLayers(hitbox, groundLayerMask);
    }

    private void OnGUI()
    {
        GUILayout.Label("rigidBody.linearVelocity = " + rigidBody.linearVelocity);
        GUILayout.Label("isGrounded = " + isGrounded);
    }
}