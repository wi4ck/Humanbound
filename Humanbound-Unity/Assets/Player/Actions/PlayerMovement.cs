using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody PBody;
    private Collider playerCollider;
    [SerializeField] private Vector2 Movement;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float normalJumpForce = 5f;
    [SerializeField] private float highJumpForce = 8f; 

    private bool isJumpHeld = false;
    private bool jumpRequested = false;

    private void Awake()
    {
        PBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerCollider = GetComponent<Collider>();
        PBody.useGravity = true; 
        PBody.freezeRotation = true;
    }
    
    private void Update()
    {
        if (Movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (Movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (isJumpHeld && GetIsGrounded() && PBody.linearVelocity.y <= 0.1f)
        {
            jumpRequested = true;
        }
    }
    
    public void SetMovement(InputAction.CallbackContext value)
    {
        Movement = value.ReadValue<Vector2>();
    }
    
    public void SetJump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isJumpHeld = true;

            if (GetIsGrounded())
            {
                jumpRequested = true;
            }
        }
        else if (value.canceled)
        {
            isJumpHeld = false;
        }
    }

    private bool GetIsGrounded()
    {
        if (PBody.linearVelocity.y > 0.1f) 
        {
            return false;
        }

        Vector3 boxCenter = new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y + 0.05f, playerCollider.bounds.center.z);
        Vector3 boxSize = new Vector3(playerCollider.bounds.size.x * 0.8f, 0.05f, 0.5f); 

        return Physics.BoxCast(boxCenter, boxSize / 2f, Vector3.down, Quaternion.identity, 0.15f, LayerMask.GetMask("Ground"));
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(Movement.x * moveSpeed, PBody.linearVelocity.y, PBody.linearVelocity.z);
        PBody.linearVelocity = targetVelocity;

        if (jumpRequested)
        {
            PBody.linearVelocity = new Vector3(PBody.linearVelocity.x, 0f, PBody.linearVelocity.z);

            float activeJumpForce = isJumpHeld ? highJumpForce : normalJumpForce;
            PBody.AddForce(Vector3.up * activeJumpForce, ForceMode.Impulse);

        }   jumpRequested = false;
    }
}