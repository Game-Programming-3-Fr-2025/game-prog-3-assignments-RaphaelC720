using System.Collections;
using UnityEngine;

public class PlayerScript3 : MonoBehaviour
{
    public Vector2 dragStartWorldPos;
    public Vector2 currentMouseWorld;
    //
    public float playerRawMag;
    public float power;
    public Vector2 direction;
    private float clampedPower;   
    public Vector2 launchDir;     
    public float maxPullDistance;
    [SerializeField] public float minImpulse = 0f;        
    [SerializeField] public float maxImpulse = 10f;     
    [SerializeField] float grabRadius = 1f;

    [SerializeField] private InfectedTilemap infectedTilemap;
    [SerializeField] private Vector2 feetOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float feetRadius = 0.10f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    public bool isLaunched;
    private int timesLaunched;
    Vector2 checkpointPosition;

    public int coinsCollected = 0;
    public int deathCount;
    public bool isDead;
    public bool doubleLaunch;
    public SpriteRenderer mySR;
    public Rigidbody2D myRB;
    public PlayerStates state = PlayerStates.Idle;
    public FlagScript flag;
    
    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        flag = FindAnyObjectByType<FlagScript>();
        infectedTilemap = FindAnyObjectByType<InfectedTilemap>();

        checkpointPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (state == PlayerStates.Idle || (isLaunched == true && timesLaunched == 1)))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
            if (Vector2.Distance(mouseWorld, transform.position) <= grabRadius)
            {
                state = PlayerStates.Launching;

                dragStartWorldPos = transform.position;
                myRB.linearVelocity = Vector2.zero;
                myRB.angularVelocity = 0f;
                clampedPower = 0f;
                launchDir = Vector2.zero; 
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && state == PlayerStates.Launching)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            currentMouseWorld = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 rawPull = dragStartWorldPos - currentMouseWorld;
            float rawMag = rawPull.magnitude;


            clampedPower = Mathf.Min(rawMag, maxPullDistance);
            playerRawMag = clampedPower;    
            if (rawMag > 0.0001f)
                launchDir = rawPull.normalized;
            else    
                launchDir = Vector2.zero;

            //launchDir = (rawMag > 0.0001f) ? (rawPull / rawMag) : Vector2.zero; 
            //direction = launchDir;                   
            //power = clampedPower;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && state == PlayerStates.Launching)
        {
            if (clampedPower > 0.01f && launchDir != Vector2.zero)
            {
                myRB.linearVelocity = Vector2.zero;

                float pull01 = clampedPower / maxPullDistance;   
                pull01 = Mathf.Clamp01(pull01);

                float impulseMag = Mathf.Lerp(minImpulse, maxImpulse, pull01);

                Vector2 impulse = launchDir * impulseMag;
                myRB.AddForce(impulse, ForceMode2D.Impulse);
            }
            state = PlayerStates.Launched;
            isLaunched = true;
            if (doubleLaunch == true)
            {
                timesLaunched += 1;
            }
            mySR.color = Color.white;
        }

        Vector2 feet = (Vector2)transform.position + feetOffset;
        isGrounded = Physics2D.OverlapCircle(feet, feetRadius, groundLayer);

        if (isGrounded && infectedTilemap != null)
        {
            infectedTilemap.InfectAtWorldPos(feet);
        }


        if (transform.position.y < -10)
            die();

        switch (state)
        {
            case PlayerStates.Idle:
                isLaunched = false;
                mySR.color = Color.white;
                timesLaunched = 0;
                clampedPower = 0f;
                break;

            case PlayerStates.Launching:
                break;

            case PlayerStates.Launched:
                if (myRB.linearVelocity.magnitude < 0.05f)
                {
                    state = PlayerStates.Idle;
                }
                break;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coinsCollected++;
        }
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPosition = pos;
    }
    void die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        if (isDead) yield break;
        isDead = true;

        myRB.linearVelocity = Vector2.zero;
        myRB.simulated = false;
        transform.localScale = Vector2.zero;

        yield return new WaitForSeconds(duration);
        deathCount += 1;

        if (infectedTilemap != null)
            infectedTilemap.RestoreAllToStart();

        transform.position = checkpointPosition;
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        myRB.simulated = true;
        state = PlayerStates.Idle;
        isDead = false;
    }
}

public enum PlayerStates
{
    Idle,
    Launching,
    Launched
}
