using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public float astroidSpeed;
    public int health;

    public GameObject explosionPrefab;

    private Rigidbody2D astroidRB;
    private rocketScript rocket;
    private GameManager2 GM;
    void Start()
    {
        astroidRB = GetComponent<Rigidbody2D>();
        rocket = FindAnyObjectByType<rocketScript>();
        GM = FindAnyObjectByType<GameManager2>();
    }

    void Update()
    {
        if (transform.position.x >= 0)
        {
            transform.Translate(transform.right * -astroidSpeed * Time.deltaTime);
        }
        else if (transform.position.x <= 0)
        {
            transform.Translate(transform.right * astroidSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("rocket"))
        {
            rocket.currentHealth -= 10;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            destroyed();
    }

    void destroyed()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject); 
        GM.astroidDestroyedCount ++;
    }
}