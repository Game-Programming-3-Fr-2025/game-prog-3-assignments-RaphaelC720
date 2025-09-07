using UnityEngine;

public class PlayerScript2 : MonoBehaviour
{
    public float moveSpeed;
    public float lerpFactor;
    private float moveInput;
    private float currentVelocity;
    public bool gravityFlipped = false;
    public SpriteRenderer mySR;
    public Rigidbody2D myRB;
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.gravityScale = 2;

        mySR = GetComponent<SpriteRenderer>();
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

        moveInput = Input.GetAxisRaw("Horizontal");
        float targetVelocity = moveInput * moveSpeed;
        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, Time.deltaTime * lerpFactor);
        transform.position += new Vector3(currentVelocity, 0, 0) * Time.deltaTime;
    }
}
