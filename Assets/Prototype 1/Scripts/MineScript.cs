using PrototypeOne;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private SpriteRenderer SR;
    PlayerScript ps;

    private void Start()
    {
        ps = FindAnyObjectByType<PlayerScript>();
        SR = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            explosion();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SR.color = new Color(0.125f, 0, 0);
    }
    void explosion()
    {
        ps.currentHealth -= 25;
        SR.color = Color.yellow;
    }
}
