using System.Collections;
using UnityEngine;

public class PlayerScript3 : MonoBehaviour
{
    private Vector3 dragStartWorldPos;
    public Vector3 currentMouseWorld;
    private Vector3 pullVector;
    public float power;
    public Vector3 direction;
    private float clampedPower;   
    private Vector3 launchDir;     
    public float maxPullDistance;
    [SerializeField] float minImpulse = 4f;        
    [SerializeField] float maxImpulse = 18f;     

    [SerializeField] float grabRadius = 1f;

    [SerializeField] private InfectedTilemap infectedTilemap;
    [SerializeField] private Vector2 feetOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float feetRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    Vector2 checkpointPosition;

    public SpriteRenderer mySR;
    public Rigidbody2D myRB;

    public PlayerStates state = PlayerStates.Idle;

    public FlagScript flag;
    
    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        flag = FindAnyObjectByType<FlagScript>();

        checkpointPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && state == PlayerStates.Idle)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);

            if (Vector2.Distance(mouseWorld, transform.position) <= grabRadius)
            {
                state = PlayerStates.Launching;
                dragStartWorldPos = transform.position; 
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && state == PlayerStates.Launching)
        {
            Vector3 mousePos2 = Input.mousePosition;
            mousePos2.z = 10f;
            currentMouseWorld = Camera.main.ScreenToWorldPoint(mousePos2);

            Vector3 rawPull = transform.position - currentMouseWorld;

            float rawMag = rawPull.magnitude;
            float usedMag = Mathf.Min(rawMag, maxPullDistance);

            // store for release
            clampedPower = usedMag;                 
            launchDir = (rawMag > 0.0001f) ? (rawPull / rawMag) : Vector3.zero; 
            direction = launchDir;                   
            power = clampedPower;

            Debug.Log($"RawMag: {rawMag:F2}, Clamped: {clampedPower:F2}, Dir: {launchDir}");


        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && state == PlayerStates.Launching)
        {
            if (clampedPower > 0.01f && launchDir != Vector3.zero)
            {
                myRB.linearVelocity = Vector2.zero;

                float pull01 = clampedPower / maxPullDistance;   
                pull01 = Mathf.Clamp01(pull01);

               
                float curved = 1f - Mathf.Pow(1f - pull01, 2f);   

                float impulseMag = Mathf.Lerp(minImpulse, maxImpulse, curved);

                Vector2 impulse = (Vector2)(launchDir * impulseMag) * myRB.mass;
                myRB.AddForce(impulse, ForceMode2D.Impulse);
            }
            state = PlayerStates.Launched;
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
        myRB.linearVelocity = Vector2.zero;
        myRB.simulated = false;
        transform.localScale = Vector2.zero;

        yield return new WaitForSeconds(duration);

        if (infectedTilemap != null)
            infectedTilemap.RestoreAllToStart();

        transform.position = checkpointPosition;
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        myRB.simulated = true;
        state = PlayerStates.Idle;
    }
}

public enum PlayerStates
{
        Idle,
        Launching,
        Launched
}
