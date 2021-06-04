using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class CharacterController : MonoBehaviour
    {
        [Range(.5f, 10.0f)]
        public float MovementSpeed = 5.0f;
        [Range(.5f, 10.0f)]
        public float JumpForce = 5.0f;
        public float InAirMovementMulitplier = 0.2f;

        private Rigidbody2D _rigidBody;
        private bool _isJumping = false;
        private bool _isGrounded = true;

        private float _horizontalMovement = 0f;
        private float _jumpingHozitonalVector = 0f;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _isGrounded = Mathf.Abs(_rigidBody.velocity.y) < 0.001;

            _horizontalMovement = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }
        }

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
