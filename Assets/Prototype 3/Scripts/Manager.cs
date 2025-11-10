using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int coins;
    public List<Image> CoinImages;
    public GameObject levelSelectPanel;
    public TextMeshProUGUI deathCountText;
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

        deathCountText.text = player.deathCount.ToString();

        if (levelSelectPanel != null && levelSelectPanel == true)
        {
            if (coins == 3)
            {
                CoinImages[2].color = Color.yellow;
            }
            else if (coins == 2)
            {
                CoinImages[1].color = Color.yellow;

            }
            else if (coins == 1)
            {
                CoinImages[0].color = Color.yellow;              
            }
        }
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
