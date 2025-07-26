using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMove : MonoBehaviour
{

    public string actionMap;
    public float speed;

    private Rigidbody2D rb;
    private InputAction m_MoveAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        m_MoveAction = InputSystem.actions.FindAction(actionMap + "/Move");
        m_MoveAction.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = m_MoveAction.ReadValue<Vector2>().x;
        rb.AddForce(new Vector2(x * speed, 0f));
    }

}
