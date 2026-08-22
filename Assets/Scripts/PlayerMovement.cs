using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Transform aimTransform;

    float movementSpeed = 4.0f;
    Rigidbody2D rb;
    Vector2 movementInput;
    Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movementInput * movementSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Swing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Swing");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            mousePos.z = transform.position.z;

            Vector3 aimDirection = (mousePos - transform.position).normalized;

            float radius = 1.0f;

            aimTransform.position = transform.position + aimDirection * radius;

            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 180f;
            aimTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
