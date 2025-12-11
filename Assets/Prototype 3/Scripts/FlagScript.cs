using UnityEngine;

public class FlagScript : MonoBehaviour
{
    PlayerScript3 player;
    public ParticleSystem flagPS;


    private void Awake()
    {
        player = FindAnyObjectByType<PlayerScript3>();
    }
    public void Start()
    {
        flagPS = GetComponent<ParticleSystem>();       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flagPS.Play();
            player.UpdateCheckpoint(transform.position);
        }
    }
}
