using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager2 : MonoBehaviour
{
    public List<GameObject> Astroids = new List<GameObject>();
    public List<Transform> astroidSpawnPoints;
    public float TimeToSpawn = 2.5f;
    public float Timer;

    public int astroidDestroyedCount = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        Timer = TimeToSpawn;
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            spawnAstroids(); 
            Timer = TimeToSpawn;
        }

        if (astroidDestroyedCount >= 100)
        {
            TimeToSpawn = 1.5f;
        }
        else if (astroidDestroyedCount >= 75)
        {
            TimeToSpawn = 1.75f;
        }
        else if (astroidDestroyedCount >= 50)
        {
            TimeToSpawn = 2f;
        }
        else if (astroidDestroyedCount >= 25)
        {
            TimeToSpawn = 2.25f;
        }
        else
        {
            TimeToSpawn = 2.5f;
        }

        scoreText.text = astroidDestroyedCount.ToString();

    }

    void spawnAstroids()
    {
        int randomAstroid = Random.Range(0, Astroids.Count);
        int randomSpawnPoint =  Random.Range(0,astroidSpawnPoints.Count);

        GameObject spawnedAstroid = Instantiate(Astroids[randomAstroid], astroidSpawnPoints[randomSpawnPoint]);
    }
}
