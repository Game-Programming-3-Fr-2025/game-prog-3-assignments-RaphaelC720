using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody2D arrowRB;

    public float speed;

    private void Start()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        arrowRB.linearVelocity = -transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("Player"))
       {
           Destroy(gameObject);
       }
    }
}
