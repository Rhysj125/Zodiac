using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PiscesMovementScript : MonoBehaviour
    {
        // Settings
        [Range(1, 10)]
        public float AttackCooldown = 10f;

        // External components
        public GameObject Player;

        // Internal components
        private Rigidbody2D _rigidBody;

        // fields
        private bool _attacking = false;
        private float _attackStartingYPosition = -99999;
        private float _nextTimeToAttack = 0f;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var rng = Random.value;

            if (Time.time >= _nextTimeToAttack && rng > 0.95)
            {
                _attacking = true;
                InitiateJumpOverAttack();
            }

            if (_attacking)
            {
                if(gameObject.transform.position.y < _attackStartingYPosition)
                {
                    _attacking = false;
                }
            }

            if (!_attacking)
            {
                _rigidBody.mass = 0;
                _rigidBody.gravityScale = 0;
                _rigidBody.velocity = new Vector2(-0.9f, 0);
            }
        }

        private void InitiateJumpOverAttack()
        {
            _rigidBody.velocity = new Vector2(0, 0);
            _rigidBody.mass = 1;
            _rigidBody.gravityScale = 1;

            _attackStartingYPosition = gameObject.transform.position.y - 1;

            var playerXPosition = Player.transform.position.x;

            gameObject.transform.position = new Vector3(playerXPosition, gameObject.transform.position.y);

            _rigidBody.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);

            _nextTimeToAttack = Time.time + AttackCooldown;
        }
    }
}
