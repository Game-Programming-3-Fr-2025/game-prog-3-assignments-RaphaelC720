using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompletePanel;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelCompletePanel.SetActive(true);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Level Selection");
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }    
    public void Level4()
    {
        SceneManager.LoadScene("Level 4");
    }
}
