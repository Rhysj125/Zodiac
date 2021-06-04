using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class CharacterController : MonoBehaviour
    {
        // Settings
        [Range(.5f, 10.0f)]
        public float MovementSpeed = 5.0f;
        [Range(.5f, 10.0f)]
        public float JumpForce = 5.0f;
        [Range(.1f, .9f)]
        public float InAirMovementMulitplier = 0.2f;

        // Components
        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;

        // States
        private bool _isJumping = false;

        // Movement Vectors
        private float _horizontalMovement = 0f;
        private float _jumpingHozitonalVector = 0f;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        // Use for inputs
        public void Update()
        {
            MoveHorizontally(Input.GetAxisRaw("Horizontal"));

            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }
        }

        private void MoveHorizontally(float movementDirection)
        {
            if (movementDirection > 0)
            {
                // Having to reposition the collider due to the sprite not being quite centre.
                _boxCollider.offset = new Vector2(0.12f, 0);
                _spriteRenderer.flipX = true;
            }
            else if (movementDirection < 0)
            {
                _boxCollider.offset = new Vector2(-0.12f, 0);
                _spriteRenderer.flipX = false;
            }

            if (!IsGrounded)
            {
                transform.position += new Vector3(_jumpingHozitonalVector + (movementDirection * InAirMovementMulitplier), 0) * Time.deltaTime * MovementSpeed;
            }
            else
            {
                transform.position += new Vector3(movementDirection, 0) * Time.deltaTime * MovementSpeed;
            }

            _horizontalMovement = movementDirection;
        }

        // Use mostly for rigid body force movement
        public void FixedUpdate()
        {          
            if (_isJumping && IsGrounded)
            {
                _isJumping = false;
                _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                _jumpingHozitonalVector = _horizontalMovement;
            }
        }

        private bool IsGrounded 
            => Mathf.Abs(_rigidBody.velocity.y) < 0.001;
    }
}
