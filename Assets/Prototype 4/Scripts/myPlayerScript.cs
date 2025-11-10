using System.Collections;
using UnityEngine;

public class myPlayerScript : MonoBehaviour
{
    public float moveSpeed;
    Vector2 inputVector;

    public float currentHealth;
    public bool died;

    public GameObject rock;
    public int playerDmg;

    public int doorCounter = 0;

    public Camera myCamera;
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    private ScorpionScript enemy1;
    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = myGameManager.Instance.myMaxHealth;
        moveSpeed = myGameManager.Instance.mySpeed;
        playerDmg = myGameManager.Instance.myDamage;
        died = false;
        myCamera.transform.position = new Vector3 (0, 0, -10);
        transform.position = new Vector2(-7, 0);
        doorCounter = 0;
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (currentHealth <= 0 && died == false)
        {
            die();
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = inputVector.normalized * moveSpeed * Time.fixedDeltaTime;
        myRB.MovePosition(myRB.position + move);
    }

    public void playertakedmg(int dmg)
    {
        currentHealth -= dmg;
        StartCoroutine(damageTaken(0.5f));
    }

    IEnumerator damageTaken(float colorChangeInterval)
    {
        mySR.color = Color.red;
        yield return new WaitForSeconds(colorChangeInterval);
        mySR.color = Color.white;
    }

    void die()
    {
        Destroy(gameObject);
        died = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hazard"))
        {
            currentHealth -= 40;
            StartCoroutine(damageTaken(0.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
            doorCounter++;

            if (doorCounter == 4)
            {
                transform.position = new Vector2(73, -3f);
                myCamera.transform.position = new Vector3(80, 0, -10);
            }
            else if (doorCounter == 3)
            {
                transform.position = new Vector2(53, 0);
                myCamera.transform.position = new Vector3(60, 0, -10);
            }
            else if (doorCounter == 2)
            {
                transform.position = new Vector2(33, 0);
                myCamera.transform.position = new Vector3(40, 0, -10);
            }
            else if (doorCounter == 1)
            {
                transform.position = new Vector2(13, -2.5f);
                myCamera.transform.position = new Vector3(20, 0, -10);
            }
        }
        if (collision.gameObject.CompareTag("arrow"))
        {
            currentHealth -= 10;
            StartCoroutine(damageTaken(0.5f));
        }
    }
}
