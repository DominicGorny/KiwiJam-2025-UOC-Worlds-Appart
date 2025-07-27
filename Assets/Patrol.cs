using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Vector2 pointA;       // Left patrol point
    public Vector2 pointB;       // Right patrol point
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make sure gravity doesn't affect the object
        rb.gravityScale = 0;

        // Start moving toward pointB
        target = pointB;
    }

    void FixedUpdate()
    {
        // Calculate direction to the target on X only
        float direction = Mathf.Sign(target.x - transform.position.x);

        // Set velocity to move horizontally
        rb.linearVelocity = new Vector2(direction * speed, 0);

        // Check if close to the target (within 0.1f units)
        if (Mathf.Abs(transform.position.x - target.x) < 0.1f)
        {
            // Switch target to the other point
            target = target == pointA ? pointB : pointA;
        }
    }
}
