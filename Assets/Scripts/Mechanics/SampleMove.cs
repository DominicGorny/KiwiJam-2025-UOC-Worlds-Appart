using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class SampleMove : MonoBehaviour
{

    public string actionMap;
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private InputAction m_MoveAction;
    private InputAction m_JumpAction;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        m_MoveAction = InputSystem.actions.FindAction(actionMap + "/Move");
        m_MoveAction.Enable();

        m_JumpAction = InputSystem.actions.FindAction(actionMap + "/Jump");
        m_JumpAction.Enable();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool isGrounded = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1f, LayerMask.NameToLayer("Default"))
            || Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1f, LayerMask.NameToLayer("Blue"));

        float x = m_MoveAction.ReadValue<Vector2>().x;
        rb.AddForce(new Vector2(x * speed, 0f));

        if(m_JumpAction.WasPerformedThisFrame() && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        Vector3 prevScale = transform.localScale;
        if (Math.Abs(rb.linearVelocityX) > 0.1) {
            transform.localScale = new Vector3(rb.linearVelocityX > 0 ? 1f : -1f, prevScale.y, prevScale.z);
        }

        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("velocityX", Math.Abs(rb.linearVelocityX));
    }

}
