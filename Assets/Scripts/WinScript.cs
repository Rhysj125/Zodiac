using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class WinScript : MonoBehaviour
    {
        public AudioSource WinMusic;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(WinCoroutine());
            }
        }

        private IEnumerator WinCoroutine()
        {
            WinMusic.Play();

            yield return new WaitUntil(() => !WinMusic.isPlaying);

            SceneManager.LoadScene("Win");
        }
    }
}
