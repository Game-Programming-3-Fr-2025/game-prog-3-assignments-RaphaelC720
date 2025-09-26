using UnityEngine;
namespace PrototypeOne
{
    public class StickScript : MonoBehaviour
    {
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager manager = FindAnyObjectByType<GameManager>();
                manager.currentSticks += 1;
                Destroy(gameObject);
            }
        }
    }
}