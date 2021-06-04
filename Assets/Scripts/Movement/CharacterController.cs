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
        public float InAirMovementMulitplier = 0.2f;

        // Components
        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;

        // States
        private bool _isJumping = false;
        private bool _isGrounded = true;

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
        void Update()
        {
            _isGrounded = Mathf.Abs(_rigidBody.velocity.y) < 0.001;

            _horizontalMovement = Input.GetAxisRaw("Horizontal");

            if(_horizontalMovement > 0)
            {
                // Deal with collider too left x offset = -0.12, right x offset = .12
                _boxCollider.offset = new Vector2(0.12f, 0);
                _spriteRenderer.flipX = true;
            }
            else if(_horizontalMovement < 0)
            {
                _boxCollider.offset = new Vector2(-0.12f, 0);
                _spriteRenderer.flipX = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }
        }

        // Use mostly for actual movement
        void FixedUpdate()
        {
            if (!_isGrounded)
            {
                transform.position += new Vector3(_jumpingHozitonalVector + (_horizontalMovement * InAirMovementMulitplier), 0) * Time.deltaTime * MovementSpeed;
            }
            else
            {
                transform.position += new Vector3(_horizontalMovement, 0) * Time.deltaTime * MovementSpeed;
            }
            

            if (_isJumping && _isGrounded)
            {
                _isGrounded = false;
                _isJumping = false;
                _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                _jumpingHozitonalVector = _horizontalMovement;
            }
        }
    }
}
