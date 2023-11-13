using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsScript : MonoBehaviour
{
    [SerializeField]
    private float Powerup_Speed = 3f;
    [SerializeField]
    private int PowerupsID;
    [SerializeField]
    private AudioClip _PowerupSound;

    // Update is called once per frame
    void Update()
    {
        Powerup_Movement();
    }

    void Powerup_Movement()
    {
        transform.Translate(Vector3.down * Powerup_Speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerScript Player = other.transform.GetComponent<PlayerScript>();
            AudioSource.PlayClipAtPoint(_PowerupSound, transform.position);
            if (Player != null)
            {
                switch (PowerupsID)
                {
                    case 0:
                        Player.Triple_ShotActive();
                        break;
                    case 1:
                        Player.SpeedPowerUp_Active();
                        break;
                    case 2:
                       Player.SheildPowerUp_Active();
                       break;
                    default:
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }

}
