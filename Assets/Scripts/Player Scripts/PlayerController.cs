using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed, tmpMovementSpeed, jumpForce, lari;
    [SerializeField] private float dropTime, dashingPower, dashingTime, dashingCooldown;
    public bool isFacingRight, isJumping;
    private bool canDash = true;
    private bool isDashing;
    private Rigidbody2D rb;
    PlayerAttack playerAttack;
    Collider2D bc;
    public GameObject winScreenUI;

    //groundchecker
    public float radius;
    public Transform groundChecker;
    public Transform groundChecker2;
    public LayerMask whatIsGround;
    public LayerMask whatIsGround2;
    public LayerMask platformLayer;

    //animation
    Animator anim;
    public bool run = false;
    public bool idle = false;
    public bool walk = false;
    public bool jump = false;
    public bool fall = false;
    public bool dash = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<Collider2D>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        Jump();
        Down();
        Run();
        Fall();
        Attack();

        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Movement();
    }

    bool IsOnGround()
    {
        return isGrounded() || isGrounded2() || isGrounded3() || Grounded() || Grounded2() || Grounded3();
    }

    private IEnumerator Dash()
{
    canDash = false;
    isDashing = true;

    dash = true;
    anim.SetBool("dash", dash);

    float originalGravity = rb.gravityScale;
    rb.gravityScale = 0f;

    float dashDirection = isFacingRight ? 1f : -1f;

    float elapsed = 0f;

    while (elapsed < dashingTime)
    {
        rb.linearVelocity = new Vector2(dashDirection * dashingPower, 0f); // Jaga kecepatan setiap frame
        elapsed += Time.deltaTime;
        yield return null; // Tunggu frame berikutnya
    }

    rb.gravityScale = originalGravity;
    isDashing = false;

    dash = false;
    anim.SetBool("dash", dash);

    yield return new WaitForSeconds(dashingCooldown);
    canDash = true;
}



    bool onWall()
    {
        return true;
    }

    public bool canShoot()
    {
        return !onWall();
    }

    

    void Movement()
    {
        if (IsAttacking()) return;
        anim.SetBool("idle", idle);
        anim.SetBool("walk", walk);
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * movementSpeed, rb.linearVelocity.y);


        if (move != 0 && lari > movementSpeed)
        {
            idle = false;
            walk = true;
        }
        else if (move != 0 && lari == movementSpeed)
        {
            idle = false;
            walk = false;
        }
        else if (move == 0 && fall)
        {
            idle = false;
        }
        else
        {
            idle = true;
            walk = false;
        }

        if (move > 0 && !isFacingRight)
            {
                transform.eulerAngles = Vector2.zero;
                isFacingRight = true;
            }
            else if (move < 0 && isFacingRight)
            {
                transform.eulerAngles = Vector2.up * 180;
                isFacingRight = false;
            }
    }

    void Run()
    {
        if (IsAttacking()) return;
        anim.SetBool("run", run);
        float move = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tmpMovementSpeed = movementSpeed;
            movementSpeed += 5;
            if (move != 0)
            {
                fall = false;
                run = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
            movementSpeed = tmpMovementSpeed;
        }
    }

    void Attack()
    {
        if (IsAttacking()) return;
        if (!playerAttack.CanAttack()) return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("attack");
            playerAttack.Attack();
        }
    }

    void Down()
    {
        if (Input.GetKeyDown(KeyCode.S) && IsOnGround())
        {
            DropDownThroughPlatform();
        }
    }

    void Jump()
    {
        if (IsAttacking()) return;
        anim.SetBool("jump", jump);
        if (Input.GetKeyDown(KeyCode.W) && IsOnGround())
        {
            rb.linearVelocity = Vector2.up * jumpForce;
            // soundmanager.Instance.PlaySound("CatJump");
        }

        if (isJumping == true)
        {
            jump = true;
        }

        if (!isJumping && !IsOnGround())
        {
            isJumping = true;
        }
        else if (isJumping && IsOnGround())
        {
            jump = false;
            isJumping = false;
        }
    }

    void Fall()
    {
        if (IsAttacking()) return;
        anim.SetBool("fall", fall);
        float move = Input.GetAxisRaw("Horizontal");
        if (!IsOnGround() && rb.linearVelocityY < 0)
        {
            jump = false;
            run = false;
            walk = false;
            fall = true;
        }
        else if (move != 0 && movementSpeed == tmpMovementSpeed + 5)
        {
            fall = false;
            idle = false;
            run = true;
        }
        else
        {
            fall = false;
        }
        
    }

    void DropDownThroughPlatform()
    {
        Vector2 boxSize = new Vector2(0.8f, 2f);
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - 0.5f); // Sedikit di bawah player

        Collider2D[] platforms = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);
        foreach (var platform in platforms)
        {
            Debug.Log("Menemukan platform: ");
            if (platform.CompareTag("OneWayPlatform"))
            {
                Debug.Log("S ditekan, mencoba menembus platform...");
                StartCoroutine(DisableCollision(platform));
            }
        }
    }

    bool IsAttacking()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("attack");
    }

    IEnumerator DisableCollision(Collider2D platform)
    {
        // Disable collision dengan platform
        Physics2D.IgnoreCollision(bc, platform, true);

        // Tunggu waktu awal drop
        yield return new WaitForSeconds(dropTime);

        // Maksimal waktu tunggu tambahan agar platform tidak hilang selamanya
        float maxWait = 1f;
        float waited = 0f;

        float minSafeDistance = 0.5f;

        while (waited < maxWait)
        {
            Bounds platformBounds = platform.bounds;
            Bounds playerBounds = bc.bounds;

            float distance = platformBounds.min.y - playerBounds.max.y;

            if (distance >= minSafeDistance)
            {
                break; // kepala player sudah cukup jauh
            }

            waited += Time.deltaTime;
            yield return null;
        }

        // Aktifkan kembali collider platform
        Physics2D.IgnoreCollision(bc, platform, false);
        Debug.Log("Collision platform dikembalikan");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Botol"))
        {
            GoalManager.singleton.CollectBotol();
            Destroy(collision.gameObject);
        }
    }

    int LayerMaskToLayer(LayerMask layerMask)
    {
        int layer = 0;
        int layerMaskValue = layerMask.value;
        while (layerMaskValue > 1)
        {
            layerMaskValue = layerMaskValue >> 1;
            layer++;
        }
        return layer;
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, radius, whatIsGround);
    }

    bool isGrounded2()
    {
        return Physics2D.OverlapCircle(groundChecker.position, radius, whatIsGround2);
    }

    bool isGrounded3()
    {
        return Physics2D.OverlapCircle(groundChecker.position, radius, platformLayer);
    }

    bool Grounded()
    {
        return Physics2D.OverlapCircle(groundChecker2.position, radius, whatIsGround);
    }

    bool Grounded2()
    {
        return Physics2D.OverlapCircle(groundChecker2.position, radius, whatIsGround2);
    }

    bool Grounded3()
    {
        return Physics2D.OverlapCircle(groundChecker2.position, radius, platformLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, radius);
    }

    void OnDrawGizmos2()
    {
        Gizmos.DrawWireSphere(groundChecker2.position, radius);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 boxSize = new Vector2(0.8f, 2f);
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - 0.5f);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
