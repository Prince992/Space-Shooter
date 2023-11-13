using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    private float Speed = 20f;
    [SerializeField]
    private GameObject _Explosion;
    [SerializeField]
    private bool IsAsteroid1, IsAsteroid2;
    private SpawnManagerScript Spawn_Manager;
    private UIManagerScript UIManager;
    private GameManagementScript GameManager;


    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
        Spawn_Manager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagementScript>();
        if(Spawn_Manager == null)
        {
            Debug.LogError("SpawnManager is Null");
        }
        if(UIManager == null)
        {
            Debug.LogError("UIManager is Null");
        }    
        if(GameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Asteroid_Movement();
    }

    private void Asteroid_Movement()
    {
        transform.Rotate(Vector3.forward * Speed * Time.deltaTime );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.IsCo_OpMode == false)
        {
            if (other.tag == "Bullet")
            {
                Instantiate(_Explosion, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                UIManager.DisableHowToStartText();
                Spawn_Manager.StartSpawnManagers();
            }
        }
        if(GameManager.IsCo_OpMode == true)
        {
            if(IsAsteroid1 == true)
            {
                if (other.tag == "Bullet")
                {
                    Instantiate(_Explosion, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                    UIManager.IsAsteroid1Destoryed = true;
                }
            }
            if(IsAsteroid2 == true)
            {
                if (other.tag == "Bullet")
                {
                    Instantiate(_Explosion, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                    UIManager.IsAsteroid2Destroyed = true;
                }
            }
        }
    }

}

