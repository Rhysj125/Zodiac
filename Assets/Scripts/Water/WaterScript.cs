using Assets.Scripts.Settings;
using UnityEngine;

namespace Assets.Scripts.Water
{
    public class WaterScript : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<IPlayer>().Kill();
            }
        }
    }
}
