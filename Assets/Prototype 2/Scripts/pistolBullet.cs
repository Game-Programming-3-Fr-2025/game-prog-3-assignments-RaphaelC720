using UnityEngine;

public class pistolBullet : MonoBehaviour
{
    public float bulletSpeed;
    public SpriteRenderer bulletSR;
    public Rigidbody2D bulletRB;
    public AstroidScript Ascript;
    private PlayerScript2 ps;

    void Start()
    {
        bulletSR = GetComponent<SpriteRenderer>();
        bulletRB = GetComponent<Rigidbody2D>();
        ps = FindAnyObjectByType<PlayerScript2>();



        if (ps.faceLeft)
            bulletRB.linearVelocity = -transform.right * bulletSpeed;
        else
            bulletRB.linearVelocity = transform.right * bulletSpeed;
       
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("astroid"))
        {
            var asteroid = collision.GetComponentInParent<AstroidScript>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(1);
                Destroy(gameObject);

            }
        }
    }
}
