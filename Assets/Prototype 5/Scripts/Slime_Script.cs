using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime_Script : MonoBehaviour
{
    //Movemennt
    private float moveInput;
    public float moveSpeed;
    private float accelerationTime = 0.1f;
    private float velocityXSmoothing = 0f;

    //Jumping
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public bool isGrounded;

    public bool inQTE;

    public GameObject speedPowerup;
    public Vector3 PowerupPos = new Vector3(0.5f, -3, 0);

    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        myRB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (inQTE == true)
        {
            return;
        }
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            mySR.flipX = false;
        else if (moveInput < 0)
            mySR.flipX = true;

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
            {
                myRB.linearVelocity = new Vector2(myRB.linearVelocity.x, jumpForce);
                jumpBufferCounter = 0f;
                coyoteTimeCounter = 0f;
            }
        }

        if (myRB.linearVelocity.y < 0)
            myRB.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (myRB.linearVelocity.y > 0 && !Input.GetButton("Jump"))
            myRB.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

        if (transform.position.y <= -10)
        {
            die();
        }

        
    }

    void FixedUpdate()
    {
        if (inQTE == true)
        {
            return;
        }
        float targetSpeed = moveInput * moveSpeed;
        float smoothedX = Mathf.SmoothDamp(myRB.linearVelocity.x, targetSpeed, ref velocityXSmoothing, accelerationTime);
        myRB.linearVelocity = new Vector2(smoothedX, myRB.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("arrow"))
            die();
    }

    public void die()
    {
        transform.position = new Vector2(-15f, -8);
        moveSpeed = 7.5f;
        

        if (FindAnyObjectByType<SpeedScript>() == null)
            Instantiate(speedPowerup, PowerupPos, Quaternion.identity);
    }
}
