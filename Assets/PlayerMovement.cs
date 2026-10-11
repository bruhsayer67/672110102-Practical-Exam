using UnityEngine;

namespace Patawee_Hutamai
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 7f;
        public float groundDrag = 5f;

        [Header("Jumping")]
        public float jumpForce = 5f;
        public float jumpCooldown = 0.25f;
        public float airMultiplier = 0.4f;
        public float gravityScale = 2.5f;     // Multiplier for gravity (1 = normal, 2.5 = heavier fall)
        bool readyToJump;

        [Header("Ground Check")]
        public float playerHeight = 2f;
        public LayerMask whatIsGround;
        bool grounded;

        [Header("References")]
        public Transform orientation;

        private float horizontalInput;
        private float verticalInput;
        private Vector3 moveDirection;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;
        }

        void Update()
        {
            // Ground check (casts a ray straight down from the player's center)
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();

            // Handle linear damping (formerly drag) based on whether grounded
            if (grounded)
                rb.linearDamping = groundDrag;
            else
                rb.linearDamping = 0f;
        }

        void FixedUpdate()
        {
            MovePlayer();

            // Apply extra downward gravity force when airborne
            if (!grounded)
            {
                rb.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);
            }
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // When jump is pressed
            if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer()
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // Apply force differently if in the air vs on the ground
            if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            else
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            // Reset y velocity so jumping is consistent
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Apply upward impulse force
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}