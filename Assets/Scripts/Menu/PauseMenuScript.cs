using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class PauseMenuScript : MonoBehaviour
    {
        public GameObject OptionsPanel;
        public GameObject Self;

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Options()
        {
            OptionsPanel.SetActive(true);
            Self.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
