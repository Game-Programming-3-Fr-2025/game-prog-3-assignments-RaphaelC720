using PrototypeOne;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    public GameObject CampFirePanel;
    public GameManager GM;
    public PlayerScript player;

    void Start()
    {
        
        
    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GM.currentSticks == 2)
        {
            if (collision.CompareTag("Player"))
            {
                CampFirePanel.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (GM.currentSticks == 2)
        {
            if (collision.CompareTag("Player"))
            {
                CampFirePanel.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.currentHealth <= 99 && GM.CampFireActive == true)
        {
            player.currentHealth += 0.5f;

        }
    }
}
