using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    public float Speed = 4.5f; // Speed of the projectile


    // Update is called once per frame
    private void Update()
    {
        transform.position += -transform.right * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Destroy the projectile on collision
    }
}
