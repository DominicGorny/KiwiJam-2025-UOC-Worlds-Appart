using UnityEngine;

public class Patrol : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    private Rigidbody2D rb;
    private Transform current;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current = a.transform;
    }

    void Update()
    {
        Vector2 point = current.position - transform.position;
        if (current == b.transform)
        {
            rb.linearVelocityX = speed;
        } 
        else
        {
            rb.linearVelocityY = -speed;
        }
    }
}
