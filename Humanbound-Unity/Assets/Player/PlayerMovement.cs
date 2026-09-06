using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody PBody;
    private Vector2 Movement;

    private void Awake()
    {
        PBody = GetComponent<Rigidbody>();
    }
   
    
    public void SetMovement(InputAction.CallbackContext value)
    {
        Movement = value.ReadValue<Vector2>();
    }
    
    
    public void SetJump(InputAction.CallbackContext value)
    {
        
        if (value.started) 
        {
            
            PBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        
        PBody.linearVelocity = new Vector3(Movement.x * 10f, PBody.linearVelocity.y, PBody.linearVelocity.z);
    }
}