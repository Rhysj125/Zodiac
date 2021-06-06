using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Water
{
    public class WaterScript : MonoBehaviour
    {
        // Components
        private BoxCollider2D _boxCollider;
        public GameObject _player;

        void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
