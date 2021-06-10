using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Menu
{
    public class OptionsMenuScript : MonoBehaviour
    {
        // Exteranl components
        public AudioMixer Audio;
        public GameObject ReturnObject;
        public GameObject self;


        public void SetMasterVolume(float volume)
        {
            Debug.Log(volume);
            Audio.SetFloat("MasterVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            Audio.SetFloat("MusicVolume", volume);
        }

        public void SetSoundEffectsVolume(float volume)
        {
            Audio.SetFloat("SoundEffectsVolume", volume);
        }

        public void Return()
        {
            ReturnObject.SetActive(true);
            self.SetActive(false);
        }
    }
}
