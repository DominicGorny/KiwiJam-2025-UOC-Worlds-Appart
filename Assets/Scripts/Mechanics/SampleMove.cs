using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMove : MonoBehaviour
{
    public string actionMap;
    public float speed;
    public float friction;
    private Rigidbody2D rb;
    private InputAction m_MoveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        m_MoveAction = InputSystem.actions.FindAction(actionMap + "/Move");
        m_MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(
            (m_MoveAction.ReadValue<Vector2>() * speed)
        );
    }
}
