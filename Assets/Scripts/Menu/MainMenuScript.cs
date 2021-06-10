using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class MainMenuScript : MonoBehaviour
    {
        public GameObject optionMenu;
        public GameObject mainMenu;

        public void PlayGame()
        {
            SceneManager.LoadScene("PiscesRoom");
            Time.timeScale = 1;
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void OpenOptions()
        {
            optionMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }
}
