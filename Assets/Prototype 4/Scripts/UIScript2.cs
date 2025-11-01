using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript2 : MonoBehaviour
{
    public GameObject upgradePanel;
    private myPlayerScript playerScript;
    private myGameManager gameManager;
    private void Start()
    {
        playerScript = FindAnyObjectByType<myPlayerScript>();
        gameManager = FindAnyObjectByType<myGameManager>();
    }
    void Update()
    {
        if (playerScript.died)
        {
            upgradePanel.SetActive(true);
        }
    }

    public void upgradeHealth()
    {
        gameManager.myMaxHealth += 5;
        upgradePanel.SetActive(false);
        playerScript.died = false;
        SceneManager.LoadScene("project4");
    }

    public void upgradeSpeed()
    {
        gameManager.mySpeed += 1;
        upgradePanel.SetActive(false);
        playerScript.died = false;
        SceneManager.LoadScene("project4");
    }

    public void upgradeDamage()
    {
        gameManager.myDamage += 1;
        upgradePanel.SetActive(false);
        playerScript.died = false;
        SceneManager.LoadScene("project4");
    }
}
