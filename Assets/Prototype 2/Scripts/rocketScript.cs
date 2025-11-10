using UnityEngine;
using UnityEngine.SceneManagement;

public class rocketScript : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    void Start()
    {
        
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
