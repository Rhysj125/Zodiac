using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PiscesMovementScript : MonoBehaviour
    {

        // Internal components
        private Rigidbody2D _rigidBody;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rigidBody.velocity = new Vector2(-.9f, 0);
        }
    }
}
