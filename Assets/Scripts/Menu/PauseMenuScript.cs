using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class PauseMenuScript : MonoBehaviour
    {
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
