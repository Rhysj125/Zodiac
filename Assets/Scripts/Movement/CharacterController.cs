using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Movement
{
    public class CharacterController : MonoBehaviour
    {
        // Settings
        [Range(0.5f, 100.0f)]
        public float MovementSpeed = 50.0f;
        [Range(0.5f, 10.0f)]
        public float JumpForce = 5.0f;
        [Range(0.1f, 0.9f)]
        public float InAirMovementMulitplier = 5f;
        public float MaxSpeed = 10;

        // Components
        public GameObject PauseMenu;

        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;
        private Animator _animator;

        // States
        private bool _isJumping = false;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        // Use for inputs
        public void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(!PauseMenu.activeSelf);
                Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                SceneManager.LoadScene("PiscesRoom");
            }
        }

        private void MoveHorizontally(float movementDirection)
        {
            _animator.SetBool("IsMoving", IsMoving);
            float normalizedHorizontalSpeed = 0;

            if (movementDirection > 0)
            {
                // Having to reposition the collider due to the sprite not being quite centre.
                _boxCollider.offset = new Vector2(0.09f, -0.11f);
                _spriteRenderer.flipX = true;
                normalizedHorizontalSpeed = 1;
            }
            else if (movementDirection < 0)
            {
                _boxCollider.offset = new Vector2(-0.09f, -0.11f);
                _spriteRenderer.flipX = false;
                normalizedHorizontalSpeed = -1;
            }

            var smoothedMovementFactor = IsGrounded ? 1f : InAirMovementMulitplier; // how fast do we change direction?
            var xVelocity = Mathf.Lerp(Mathf.Abs(_rigidBody.velocity.x) > MaxSpeed ? MaxSpeed * normalizedHorizontalSpeed : _rigidBody.velocity.x, normalizedHorizontalSpeed * MovementSpeed, Time.deltaTime * smoothedMovementFactor);

            Debug.Log(smoothedMovementFactor);

            _rigidBody.velocity = new Vector2(xVelocity, _rigidBody.velocity.y);
        }

        // Use mostly for rigid body force movement
        public void FixedUpdate()
        {
            MoveHorizontally(Input.GetAxisRaw("Horizontal"));

            if (_isJumping && IsGrounded)
            {
                _isJumping = false;
                _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
        }

        private bool IsGrounded
            => Mathf.Abs(_rigidBody.velocity.y) < 0.001;

        private bool IsMoving
            => _rigidBody.velocity.x != 0 || _rigidBody.velocity.y != 0;
    }
}
