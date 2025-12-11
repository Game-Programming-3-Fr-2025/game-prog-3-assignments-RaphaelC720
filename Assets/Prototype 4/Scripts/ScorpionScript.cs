using UnityEngine;

public class ScorpionScript : MonoBehaviour
{
    public Transform player;      
    public float moveSpeed = 3f;   
    private float range = 13f;
    public float health = 100f;
    public int enemyDmg = 15;

    private SpriteRenderer enemySR;
    private myPlayerScript playerScript;

    private void Start()
    {
        playerScript = FindAnyObjectByType<myPlayerScript>();
        enemySR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if(distance <= range)
        {
            direction.Normalize();
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }

        if (health <= 0)
            die();
    }

    public void takedmg(int dmg)
    {
        health -= dmg;
    }

    void die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.playertakedmg(enemyDmg);
        }
    }
}
