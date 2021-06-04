using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject Player;
        public float MovementMultiplier = 0.5f;

        void Start()
        {

        }

        void FixedUpdate()
        {
            var horizontalMove = Input.GetAxisRaw("Horizontal") * MovementMultiplier;
            var verticalMove = Input.GetAxisRaw("Vertical") * MovementMultiplier;

            Player.transform.position = new Vector3(Player.transform.position.x + horizontalMove, Player.transform.position.y + verticalMove);
        }
    }
}
