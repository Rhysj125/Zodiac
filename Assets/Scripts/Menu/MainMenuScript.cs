using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class MainMenuScript : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("MainRoom");
            Time.timeScale = 1;
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
