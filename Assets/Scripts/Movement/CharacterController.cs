using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.Audio;
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
        [Range(0f, 1.0f)]
        public float InAirMovementMulitplier = 0.3f;
        [Range(0f, 1.0f)]
        public float GroundMovementMulitplier = 0.5f;
        public float MaxSpeed = 10;

        // External Components
        public GameObject PauseMenu;
        public AudioMixerGroup SoundEffectsMixerGroup;
        public AudioClip JumpSoundEffect;
        public AudioClip RunningSoundEffect;

        // Internal Components
        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;
        private Animator _animator;
        private AudioSource _jumpingAudioSource;
        private AudioSource _walkingAudioSource;

        // States
        private bool _isJumping = false;
        private float _horizontalMovement;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();

            _jumpingAudioSource = CreateSoundEffectAudioSource(JumpSoundEffect);
            _walkingAudioSource = CreateSoundEffectAudioSource(RunningSoundEffect);
        }

        private AudioSource CreateSoundEffectAudioSource(AudioClip clip)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = SoundEffectsMixerGroup;

            return audioSource;
        }

        // Use for inputs
        public void Update()
        {
            if (Input.GetButtonDown("Jump") && IsGrounded)
            {
                _isJumping = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(!PauseMenu.activeSelf);
                Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
            }

            _horizontalMovement = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                SceneManager.LoadScene("PiscesRoom");
            }
        }

        private void MoveHorizontally()
        {
            _animator.SetBool("IsMoving", IsMovingHorizontally);
            float normalizedHorizontalSpeed = 0;

            if (_horizontalMovement > 0)
            {
                // Having to reposition the collider due to the sprite not being quite centre.
                _boxCollider.offset = new Vector2(0.06f, -0.05f);
                _spriteRenderer.flipX = true;
                normalizedHorizontalSpeed = 1;
            }
            else if (_horizontalMovement < 0)
            {
                _boxCollider.offset = new Vector2(-0.06f, -0.05f);
                _spriteRenderer.flipX = false;
                normalizedHorizontalSpeed = -1;
            }

            if (IsMovingHorizontally && !_walkingAudioSource.isPlaying)
            {
                _walkingAudioSource.Play();
            }

            var smoothedMovementFactor = IsGrounded ? GroundMovementMulitplier : InAirMovementMulitplier; // how fast do we change direction?
            var xVelocity = Mathf.Lerp(Mathf.Abs(_rigidBody.velocity.x) > MaxSpeed ? MaxSpeed * normalizedHorizontalSpeed : _rigidBody.velocity.x, normalizedHorizontalSpeed * MovementSpeed, Time.deltaTime * smoothedMovementFactor);

            _rigidBody.velocity = new Vector2(xVelocity, _rigidBody.velocity.y);
        }

        // Use mostly for rigid body force movement
        public void FixedUpdate()
        {
            if (Player.PlayerInstance.IsAlive)
            {
                _animator.SetBool("IsJumping", !IsGrounded);

                MoveHorizontally();
                if (_isJumping && IsGrounded)
                {
                    HandleJump();
                }
            }
        }

        private void HandleJump()
        {
            _jumpingAudioSource.Play();
            _isJumping = false;
            _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        private bool IsGrounded
            => Mathf.Abs(_rigidBody.velocity.y) < 0.001;

        private bool IsMovingHorizontally
            => _rigidBody.velocity.x != 0;
    }
}
