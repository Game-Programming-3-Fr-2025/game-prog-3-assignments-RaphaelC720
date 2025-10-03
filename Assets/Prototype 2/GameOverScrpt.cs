using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScrpt : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown) { SceneManager.LoadScene("Project2"); }
    }
}
