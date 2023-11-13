using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _EnemySpeed = 4.5f;
    [SerializeField]
    private GameObject _EnemyExplosion, _EnemyLaser;
    private UIManagerScript UiManager;
    private float Fire_Rate = 3.0f, Can_Fire = -1f;

    void Start()
    {
        UiManager = GameObject.Find("Canvas").GetComponent<UIManagerScript>();

        if (UiManager == null)
        {
            Debug.LogError("UiManager is Null");
        }

    }
    void Update()
    {
        EnemyMovement();

        if (Time.time > Can_Fire)
        {
            Fire_Rate = Random.Range(3f, 7f);
            Can_Fire = Time.time + Fire_Rate;
            GameObject EnemyLaser = Instantiate(_EnemyLaser, transform.position, Quaternion.identity);
            BulletScript[] Bullets = EnemyLaser.GetComponentsInChildren<BulletScript>();

            for (int i = 0; i < Bullets.Length; i++)
            {
                Bullets[i].EnemyLaserActive();
            }
        }

    }
    void EnemyMovement()
    {
        float RandomX = Random.Range(-9.0f, 9.0f);
        transform.Translate(Vector3.down * _EnemySpeed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(RandomX, 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            if (UiManager != null)
            {
                UiManager.Update_Score(10);
            }

            Instantiate(_EnemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            PlayerScript player = other.transform.GetComponent<PlayerScript>();

            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_EnemyExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
