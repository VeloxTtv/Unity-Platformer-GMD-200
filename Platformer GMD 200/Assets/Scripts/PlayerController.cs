using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public UIManager uiManager;
    public AudioSource backgroundMusic;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public PlayerHealth playerHealth;

    public static int healthPoints = 4;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slideTime;
    [SerializeField] private float _slideSpeed;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    private float _moveHorizontal;
    private float _slideCooldown;
   
    public int _numStars;
    public float _isMoving;
    private bool _jumpNotDouble;
    private bool _canDoubleJump;
    private bool _facingRight = true;

    public bool _isJumping;
    public bool _isSliding;
    public bool _hasStar;

    private Rigidbody2D rb2D;
    private Animator _anim;

    public SpriteRenderer healthBarSprite;
    public Sprite[] healthSprites;

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _isJumping = false;
        _canDoubleJump = true;
        _jumpNotDouble = false;
        _slideCooldown = 0f;
        _numStars = 0;
    }

    void Update()
    {
        AnimationControllers();
        FlipController();
        JumpBuffer();
        Slide();
        Stars();
        Jump();
        Menu();

        _moveHorizontal = Input.GetAxisRaw("Horizontal");

        _slideCooldown -= Time.deltaTime;

        if (Time.timeScale == 1f)
        {
            if (AudioScript.instance != null)
            {
                AudioScript.instance.audioSource.UnPause();
            }
        } 

        if (!_isJumping)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (playerHealth.currentHealth == 0)
        {
            Death();
        }
    }
    void FixedUpdate()
    {
        if (_isSliding) return;

        if (_moveHorizontal > 0.1f || _moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
        }
        else
        {
            float newX = rb2D.linearVelocity.x * 0.75f;
            if (Mathf.Abs(newX) < 0.01f) newX = 0f;
            rb2D.linearVelocity = (new Vector2(newX, rb2D.linearVelocity.y));
        }
    }

    public void LoadNewSceneAdditive()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    void Menu()
    {
        if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton7)) && Time.timeScale == 1f)
        {
            if (AudioScript.instance != null)AudioScript.instance.audioSource.Pause();
            LoadNewSceneAdditive();
        }
    }

    void JumpBuffer()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void Stars()
    {
        if (_numStars > 3) _numStars = 3;
        else if (_numStars < 0) _numStars = 0;
        if (_numStars == 0) _hasStar = false;

        if (_numStars == 0)
        {
            Star1.SetActive(false);
            Star2.SetActive(false);
            Star3.SetActive(false);
        }
        else if (_numStars == 1)
        {
            Star1.SetActive(true);
            Star2.SetActive(false);
            Star3.SetActive(false);
        }
        else if (_numStars == 2)
        {
            Star2.SetActive(true);
            Star3.SetActive(false);
        }
        else if (_numStars == 3)
        {
            Star3.SetActive(true);
        }
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0)) && !_isSliding)
        {
            Vector2 effect = new Vector2(0.011f, -1f);
            Vector2 player = new Vector2(transform.position.x, transform.position.y);

            if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, _jumpForce);
                _isJumping = true;
                _jumpNotDouble = true;
                _jumpBufferCounter = 0f;

                _anim.SetBool("isJumping", _isJumping);
                _anim.SetBool("jumpNotDouble", _jumpNotDouble);
            }
            else if (_canDoubleJump || _hasStar)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, _jumpForce);
                _jumpNotDouble = false;

                _anim.Play("IdlePlayer");
                _anim.ResetTrigger("DoubleJump");
                _anim.SetTrigger("DoubleJump");
                _anim.SetBool("jumpNotDouble", _jumpNotDouble);
                GetComponent<EffectSpawner>().SpawnEffectAtLocation1(player + effect);

                if (_canDoubleJump)
                {
                    _canDoubleJump = false;
                }
                else if (_hasStar)
                {
                    _numStars--;
                    if (_numStars <= 0) _hasStar = false;
                    _canDoubleJump = false;
                }
            }
          
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, rb2D.linearVelocity.y * 0.5f);
            _coyoteTimeCounter = 0f;
        }
    }

    private void AnimationControllers()
    {
        _isMoving = Mathf.Abs(rb2D.linearVelocity.x);
        if (_isMoving < 0.01f) _isMoving = 0f;
        _anim.SetFloat("isMoving", _isMoving);
    }
    public void Death()
    {
        rb2D.linearVelocity = Vector2.zero;
        uiManager.ToggleDeathScreen();
    }

    private void FlipController()
    {
        if (rb2D.linearVelocity.x < -0.1f && _facingRight)
            Flip();
        else if (rb2D.linearVelocity.x > 0.1f && !_facingRight)
            Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Slide()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1)) && !_isJumping && !_isSliding && _slideCooldown <= 0f)
        {
            _isSliding = true;
            _anim.SetBool("isCrouching", _isSliding);

            _slideCooldown = 0.4f;
            float horizontalDirection = _facingRight ? 1 : -1;

            rb2D.linearVelocity = new Vector2(horizontalDirection * _slideSpeed, rb2D.linearVelocity.y);
            Invoke("SlideEnd", _slideTime);
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) || Input.GetKeyDown(KeyCode.JoystickButton0) || (Input.GetKeyUp(KeyCode.LeftControl) || (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.JoystickButton1))))
        {
            CancelInvoke("SlideEnd");
            _isSliding = false;
            _anim.SetBool("isCrouching", _isSliding);
        }
    }

    void SlideEnd()
    {
        _isSliding = false;
        _anim.SetBool("isCrouching", _isSliding);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isJumping = false;
            _canDoubleJump = true;
            _jumpNotDouble = false;
            _anim.SetBool("isJumping", _isJumping);
            _anim.SetBool("jumpNotDouble", _jumpNotDouble);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isJumping = true;
            _anim.SetBool("isJumping", _isJumping);
            _anim.SetBool("jumpNotDouble", _jumpNotDouble);
        }
    }

}
