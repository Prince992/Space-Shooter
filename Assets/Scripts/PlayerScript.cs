using System.Collections;
using  System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float Speed = 6f, SpeedBoost = 2f;
    [SerializeField]
    private GameObject Bullets, Triple_Shot, Sheild_Visvuals, Right_Engine, Left_Engine;
    [SerializeField]
    private AudioClip _LaserSound;
    private AudioSource audioSource;
    private float _BulletRate = 0.1f, _NextFire = -1f, InvulnerablePlayer = 0f;
    public int _PlayerLives = 3, Player1Lives = 3, Player2Lives = 3;
    private bool IsTriple_ShotActive = false, IsSpeed_PowerUpActive = false, IsSheild_PowerUpActive = false;
    public bool IsPlayer1 = false, IsPlayer2 = false;
    private GameManagementScript GameManager;
    private UIManagerScript UIManager;
    private Animator PlayerAnimation;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagementScript>();
        UIManager = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
        PlayerAnimation = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        PowerUpAtStart();
        if(GameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }
        if(UIManager == null)
        {
            Debug.LogError("UIManager is Null");
        }
        if(PlayerAnimation == null)
        {
            Debug.LogError("PlayerAnimator is Null");
        }
        if(audioSource == null)
        {
            Debug.LogError("AudioSource of Player is Null");
        }
        else
        {
            audioSource.clip = _LaserSound;
        }
        if(GameManager.IsCo_OpMode == false)
        {
            transform.position = new Vector3(0, -2, 0);
        }
        else
        {
            if(IsPlayer1 == true)
            {
                transform.position = new Vector3(-4.5f, -2, 0);
            }
            if(IsPlayer2 == true)
            {
                transform.position = new Vector3(4.5f, -2, 0);
            }
        }
        
    }
    private void PowerUpAtStart()
    {
        Sheild_Visvuals.SetActive(false);
        Left_Engine.SetActive(false);
        Right_Engine.SetActive(false);
    }
    void Update()
    {
        if (IsPlayer1 == true)
        {
            PlayeroneMovement();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _NextFire)
            {
                BulletSpawn();
            }
            Player1Animation();
        }
        if(IsPlayer2 == true)
        {
            PlayerTwoMovement();
            if (Input.GetKeyDown(KeyCode.Keypad0) && Time.time > _NextFire)
            {
                BulletSpawn();
            }
            Player2Animation();
        }
    }

    private void Player1Animation()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerAnimation.SetBool("IsRight", true);
            PlayerAnimation.SetBool("IsLeft", false);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerAnimation.SetBool("IsRight", false);
            PlayerAnimation.SetBool("IsLeft", true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            PlayerAnimation.SetBool("IsRight", false);
            PlayerAnimation.SetBool("IsLeft", false);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PlayerAnimation.SetBool("IsRight", false);
            PlayerAnimation.SetBool("IsLeft", false);
        }

    }
    private void Player2Animation()
    {
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            PlayerAnimation.SetBool("IsRight", true);
            PlayerAnimation.SetBool("IsLeft", false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            PlayerAnimation.SetBool("IsRight", false);
            PlayerAnimation.SetBool("IsLeft", true);
        }
        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            PlayerAnimation.SetBool("IsRight", false);
            PlayerAnimation.SetBool("IsLeft", false);
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            PlayerAnimation.SetBool("IsRight", false);
            PlayerAnimation.SetBool("IsLeft", false);
        }
    }
    void PlayeroneMovement()
    {
        float Horizontal_Input = Input.GetAxis("Horizontal");
        float Vertical_Input = Input.GetAxis("Vertical");
        Vector3 Movement = new Vector3(Horizontal_Input, Vertical_Input, 0);

        if (IsSpeed_PowerUpActive == true)
        {
            transform.Translate(Movement * (SpeedBoost * Speed) * Time.deltaTime);
        }
        else
        {
            transform.Translate(Movement *Speed* Time.deltaTime);
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0f), 0);

        if(transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if(transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void PlayerTwoMovement()
    {
        if (IsSpeed_PowerUpActive == true)
        {
            
            if(Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * (SpeedBoost * Speed) * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * (SpeedBoost * Speed) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad5))
            {
                transform.Translate(Vector3.down * (SpeedBoost * Speed) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * (SpeedBoost * Speed) * Time.deltaTime);
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * Speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad5))
            {
                transform.Translate(Vector3.down * Speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left *  Speed * Time.deltaTime);
            }

        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0f), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void BulletSpawn()
    {
        _NextFire = Time.time + _BulletRate;
        if (IsTriple_ShotActive == true)
        {
            Instantiate(Triple_Shot, transform.position + new Vector3(-1,0,0), Quaternion.identity);
        }
        else
        {
            Instantiate(Bullets, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }

        audioSource.Play();
    }
    public void Damage()
    {
        if (GameManager.IsCo_OpMode == false)
        {
            if (IsSheild_PowerUpActive == true)
            {
                IsSheild_PowerUpActive = false;
                Sheild_Visvuals.SetActive(false);
                return;
            }

            else
            {
                _PlayerLives--;
                if (_PlayerLives == 2)
                {
                    Right_Engine.SetActive(true);
                }
                else if (_PlayerLives == 1)
                {
                    Left_Engine.SetActive(true);
                }
                UIManager.Update_Lives_Img(_PlayerLives);
            }

            if (_PlayerLives < 1)
            {
                Destroy(this.gameObject);
            }
        }

        if(GameManager.IsCo_OpMode == true)
        {
            if(IsPlayer1 == true)
            {
                if (IsSheild_PowerUpActive == true)
                {
                    IsSheild_PowerUpActive = false;
                    Sheild_Visvuals.SetActive(false);
                    return;
                }

                else
                {
                    Player1Lives--;
                    if (Player1Lives == 2)
                    {
                        Right_Engine.SetActive(true);
                    }
                    else if (Player1Lives == 1)
                    {
                        Left_Engine.SetActive(true);
                    }
                    UIManager.Update_Player1_Lives(Player1Lives);
                }

                if (Player1Lives < 1)
                {
                    Destroy(this.gameObject);
                }
            }
            if(IsPlayer2 == true)
            {
                if (IsSheild_PowerUpActive == true)
                {
                    IsSheild_PowerUpActive = false;
                    Sheild_Visvuals.SetActive(false);
                    return;
                }
                else
                {
                    Player2Lives--;
                    if (Player2Lives == 2)
                    {
                        Right_Engine.SetActive(true);
                    }
                    else if (Player2Lives == 1)
                    {
                        Left_Engine.SetActive(true);
                    }
                    UIManager.Update_Player2_Lives(Player2Lives);
                }

                if (Player2Lives < 1)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Bullet")
        {
            Destroy(other.gameObject);
            if(Time.time> InvulnerablePlayer)
            {
                InvulnerablePlayer = Time.time + 0.2f;
                Damage(); 
            }
        }
    }
    public void Triple_ShotActive()
    {
        IsTriple_ShotActive = true;
        StartCoroutine(Stop_Triple_Shot());
    }
    IEnumerator Stop_Triple_Shot()
    {
        yield return new WaitForSeconds(10.0f);
        IsTriple_ShotActive = false;
    }

    public void SpeedPowerUp_Active()
    {
        IsSpeed_PowerUpActive = true;
        StartCoroutine(BackToNormalSpeed());
    }
    IEnumerator BackToNormalSpeed()
    {
        yield return new WaitForSeconds(10.0f);
        IsSpeed_PowerUpActive = false;
    }

    public void SheildPowerUp_Active()
    {
        IsSheild_PowerUpActive = true;
        Sheild_Visvuals.SetActive(true);
        StartCoroutine(Disable_Sheild());
    }

    IEnumerator Disable_Sheild()
    {
        yield return new WaitForSeconds(10.0f);
        IsSheild_PowerUpActive = false;
        Sheild_Visvuals.SetActive(false);
    }

}
