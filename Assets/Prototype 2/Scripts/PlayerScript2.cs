using UnityEngine;

public class PlayerScript2 : MonoBehaviour
{
    public float moveSpeed;
    public float lerpFactor;
    private float moveInput;
    private float currentVelocity;

    public bool gravityFlipped = false;
    public bool faceLeft = false;
    public bool faceRight = true;

    public AudioSource AS;
    public AudioClip shootSound;

    public SpriteRenderer mySR;
    public Rigidbody2D myRB;

    public PistolScript pistol;
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.gravityScale = 2;

        mySR = GetComponent<SpriteRenderer>();
        pistol = FindAnyObjectByType<PistolScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gravityFlipped == false)
        {
            myRB.gravityScale = -2;
            gravityFlipped = true;
            mySR.flipY = true;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && gravityFlipped == true)
        {
            myRB.gravityScale = 2;
            gravityFlipped = false;
            mySR.flipY = false;
        }

        if (moveInput > 0)
        {
            faceRight = true;
            faceLeft = false;

            transform.localScale = new Vector3(2, 2, 1);
        }
        else if (moveInput < 0)
        {
            faceLeft = true;
            faceRight = false;

            transform.localScale = new Vector3(-2, 2, 1);
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        float targetVelocity = moveInput * moveSpeed;
        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, Time.deltaTime * lerpFactor);
        transform.position += new Vector3(currentVelocity, 0, 0) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pistol.shootBullet();
            AS.PlayOneShot(shootSound);
        }
    }
}
