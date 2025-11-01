using UnityEngine;

public class SpeedScript : MonoBehaviour
{
    private Slime_Script player;
    void Start()
    {
        player = FindAnyObjectByType<Slime_Script>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.moveSpeed = 12.5f;
            Destroy(gameObject);
        }
    }
}
