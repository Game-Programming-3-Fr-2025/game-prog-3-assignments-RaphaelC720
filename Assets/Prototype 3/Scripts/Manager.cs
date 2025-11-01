using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int coins;
    public static Manager Instance;
    public PlayerScript3 player;
    public void Start()
    {
        player = FindAnyObjectByType<PlayerScript3>();

        if (Instance == null)
            Instance = this;

    }

    public void Update()
    {
        coins = player.coinsCollected;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level Selection");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
