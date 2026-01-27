using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject playerGun;

    private float moveSpeed;

    private float jumpForce;
    public bool isJumping;

    private float moveHorizontal;
    private float moveVertical;
    private bool facingRight = true;
    public bool isMoving;
    public bool isShooting;

    public  bool isCrouched;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;
    
    //unfinished, ADD THIS!!
    public Sprite muzzleFlash;
    [Range(0f, 5f)]
    public int FramesToFlash = 1;

    private Rigidbody2D rb2D;
    private Animator anim;
    void Start()
    {
        StartCoroutine(DoFlash());
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveSpeed = 3f;
        jumpForce = 40f;
        isJumping = false;
        isShooting = false;
    }

    void Update()
    {
        AnimationControllers();
        FlipController();
        Crouched();

        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isJumping && !isCrouched)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            anim.SetBool("isShooting", isShooting);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            anim.SetBool("isShooting", isShooting);
        }

            anim.SetFloat("yVelocity", rb2D.linearVelocity.y);
    }

    IEnumerator DoFlash()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var originalSprite = renderer.sprite;
        renderer.sprite = muzzleFlash;

        var framesFlashed = 0;
        while (framesFlashed < FramesToFlash)
        {
            framesFlashed++;
            yield return null;
        }
    }
    private void AnimationControllers()
    {
        isMoving = rb2D.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    //got parts of my Crouched function from a yt video by Expat Studios
    private void Crouched()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl)) && !isJumping)
        {
            isCrouched = true;
            anim.SetBool("isCrouching", isCrouched);
            moveSpeed = 0f;
            playerGun.transform.localPosition = new Vector2(0.2256f, -0.1598f);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouched = false;
            anim.SetBool("isCrouching", isCrouched);
            moveSpeed = 3f;
            playerGun.transform.localPosition = new Vector2(0.2256f, -0.01081914f);
        }
    }
    void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FlipController()
    {
        if (rb2D.linearVelocity.x < 0 && facingRight)
            Flip();
        else if (rb2D.linearVelocity.x > 0 && !facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

}
