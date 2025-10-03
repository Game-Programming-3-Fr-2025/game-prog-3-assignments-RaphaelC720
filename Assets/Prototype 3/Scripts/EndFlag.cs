using TMPro;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.SetActive(true);
        particle1.SetActive(true);
        particle2.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        button.SetActive(false);
        particle1.SetActive(false);
        particle2.SetActive(false);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("project3");
    }
}
