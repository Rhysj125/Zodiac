﻿using Assets.Scripts.Settings;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PiscesMovementScript : MonoBehaviour
    {
        // Settings
        [Range(1, 10)]
        public float AttackCooldown = 10f;
        [Range(1,10)]
        public float SwimSpeed = 2f;
        public int Direction = -1;

        // External components
        public GameObject Player;

        // Internal components
        private Rigidbody2D _rigidBody;

        // fields
        private bool _attacking = false;
        private float _nextTimeToAttack;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _nextTimeToAttack = Time.time + AttackCooldown;
        }

        private void FixedUpdate()
        {
            var rng = Random.value;

            if (Time.time >= _nextTimeToAttack && rng > 0.95 && !_attacking)
            {
                _attacking = true;
                StartCoroutine(SwimAcross());
            }

            if (!_attacking)
            {
                _rigidBody.mass = 0;
                _rigidBody.gravityScale = 0;
                _rigidBody.velocity = new Vector2(Direction*SwimSpeed, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<IPlayer>().TakeDamage();
            }
        }

        private IEnumerator SwimAcross()
        {
            _rigidBody.mass = 0;
            _rigidBody.gravityScale = 0;
            _rigidBody.velocity = new Vector2(Direction*SwimSpeed, 0);

            var yPosition = GetComponentInParent<Camera>().transform.position.y - 5;
            var xPosition = 13;

            gameObject.transform.position = new Vector3(xPosition, yPosition);

            yield return new WaitUntil(() => {
                Debug.Log($"Current X: {gameObject.transform.position.x}");
                Debug.Log($"Target X: {xPosition}");
                return gameObject.transform.position.x <= xPosition * -1;
            });

            _attacking = false;
        }

        private IEnumerator InitiateJumpUpAttack()
        {
            _rigidBody.velocity = new Vector2(0, 0);
            _rigidBody.mass = 1;
            _rigidBody.gravityScale = 1;

            var playerXPosition = Player.transform.position.x;
            var playerYPosition = Player.transform.position.y;

            gameObject.transform.position = new Vector3(playerXPosition, playerYPosition - 13);
            gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);

            _rigidBody.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);

            var startingPosition = gameObject.transform.position.y;

            _nextTimeToAttack = Time.time + AttackCooldown;

            yield return new WaitUntil(() => {
                Debug.Log("Checking pos");
                Debug.Log(startingPosition);
                Debug.Log(gameObject.transform.position.y);
                return startingPosition > gameObject.transform.position.y;
                }
            );

            _attacking = false;
        }
    }
}
