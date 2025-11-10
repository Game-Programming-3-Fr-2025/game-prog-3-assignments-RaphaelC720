using UnityEngine;

public class FlagScript : MonoBehaviour
{
    PlayerScript3 player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerScript3>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.UpdateCheckpoint(transform.position);
        }
    }
}
