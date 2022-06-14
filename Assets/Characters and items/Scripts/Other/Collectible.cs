using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        POWERUP,
        LIFE,
    }

    public CollectibleType curCollectible;
    public AudioClip pickupSound;
    public AudioMixerGroup soundFXGroup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl curPlayer = collision.gameObject.GetComponent<PlayerControl>();
            GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(pickupSound, soundFXGroup);


            switch (curCollectible)
            {
               
                case CollectibleType.LIFE:
                    curPlayer.lives++;
                    break;
                case CollectibleType.POWERUP:
                    curPlayer.StartJumpForceChange();
                    break;
            }

            Destroy(gameObject);
        }
    }
}
