using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Settings
{
    public class Player : MonoBehaviour, IPlayer
    {
        // Singleton instance
        private static Player _player = null;

        // External Components
        public AudioMixerGroup SoundEffectsMixerGroup;
        public AudioClip DamagedSoundEffect;
        public AudioClip DeathSoundEffect;

        // Settings
        [HideInInspector]
        public int Health { get; private set; }
        [Range(0f, 2f)]
        public float InvincibilityTime = 1f;

        // Fields
        private float _invincibleTill = 0f;
        private AudioSource _damagedAudioSource;
        private AudioSource _deathAudioSource;

        public Player()
        {
            Health = 3;
        }

        public static Player PlayerInstance
        {
            get
            {
                if(_player == null)
                {
                    _player = new Player();
                }

                return _player;
            }
        }

        public void Reset()
        {
            _player = new Player();
            _invincibleTill = Time.time + InvincibilityTime;
        }

        public void TakeDamage()
        {
            if (Time.time >= _invincibleTill)
            {
                Health--;
                _damagedAudioSource = CreateSoundEffectAudioSource(DamagedSoundEffect);
                _damagedAudioSource.Play();
                _invincibleTill = Time.time + InvincibilityTime;
            }

            if(Health <= 0)
            {
                StartCoroutine(Death());
            }
        }

        public void Kill()
        {
            StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            _deathAudioSource = CreateSoundEffectAudioSource(DeathSoundEffect);
            _deathAudioSource.Play();
            yield return new WaitUntil(() => !_deathAudioSource.isPlaying);

            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");

            yield return null;
        }

        private AudioSource CreateSoundEffectAudioSource(AudioClip clip)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = SoundEffectsMixerGroup;

            return audioSource;
        }
    }
}
