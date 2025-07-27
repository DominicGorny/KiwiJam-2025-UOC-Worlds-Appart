using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public ProjectileBehaviour projectilePrefab;
        public Transform launchOffset;

        public Collider2D collider2d;
        public Health health;
        public bool controlEnabled = true;

        SpriteRenderer spriteRenderer;
        private InputAction m_shootAction;

        public Bounds Bounds;
        public string actionMap;
        public float speed = 5f;
        public float jumpForce = 500f;
        public LayerMask layer;

        private Rigidbody2D rb;
        private InputAction m_MoveAction;
        private InputAction m_JumpAction;
        internal Animator animator;
        bool isGrounded;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            health = GetComponent<Health>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            collider2d = GetComponent<Collider2D>();
            Bounds = collider2d.bounds;

            m_MoveAction = InputSystem.actions.FindAction(actionMap + "/Move");
            m_MoveAction.Enable();

            m_JumpAction = InputSystem.actions.FindAction(actionMap + "/Jump");
            m_JumpAction.Enable();

            m_shootAction = InputSystem.actions.FindAction(actionMap + "/Attack");
            m_shootAction.Enable();
        }

        void Update()
        {
            if (controlEnabled)
            {
                
                // Movement
                float x = m_MoveAction.ReadValue<Vector2>().x;
                rb.AddForce(new Vector2(x * speed, 0f));

                // Jumping
                int defaultLayerMask = 1 << LayerMask.NameToLayer("Default");
                bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, layer | defaultLayerMask);
                Debug.DrawRay(transform.position, Vector2.down, Color.red);


                if (m_JumpAction.WasPerformedThisFrame() && isGrounded)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                }

                // Face the direction we're moving
                Vector3 prevScale = transform.localScale;
                if (Math.Abs(rb.linearVelocityX) > 0.1)
                {
                    transform.localScale = new Vector3(rb.linearVelocityX > 0 ? 1f : -1f, prevScale.y, prevScale.z);
                }

                // Update the animator on our whereabouts
                animator.SetBool("isGrounded", isGrounded);
                animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocityX));

                // Shooting
                if (m_shootAction.WasPressedThisFrame())
                {
                    Vector3 adjustedLaunchOffset;
                    Quaternion adjustedRotation;

                    Debug.Log("Shoot");

                    if (transform.localScale.x < 0)
                    {
                        adjustedLaunchOffset = new Vector3(-launchOffset.localPosition.x, launchOffset.localPosition.y, launchOffset.localPosition.z);
                        adjustedRotation = Quaternion.AngleAxis(0f, Vector3.forward);

                    }
                    else
                    {
                        adjustedLaunchOffset = new Vector3(launchOffset.localPosition.x, launchOffset.localPosition.y, launchOffset.localPosition.z);
                        adjustedRotation = Quaternion.AngleAxis(180f, Vector3.forward);
                    }


                    adjustedLaunchOffset += transform.position;

                    Instantiate(projectilePrefab, adjustedLaunchOffset, adjustedRotation);
                }
            }
        }



    }
}