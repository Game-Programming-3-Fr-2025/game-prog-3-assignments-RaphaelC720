using PrototypeOne;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI currentStickText;

    public GameObject StickTextPanel;
    public GameObject CampfiretxtPanel;
    public GameObject Arrow;

    public Image fillbar;

    public PlayerScript player;
    public GameManager manager;
    public void Update()
    {
        UpdateStickText();
        if (manager.currentSticks == 2)
        {
            CampfiretxtPanel.SetActive(true);
            Arrow.SetActive(true);
        }

        float healthPercent = Mathf.Clamp01(player.currentHealth / player.maxHealth);
        fillbar.fillAmount = healthPercent;
    }
    public void UpdateStickText()
    {
        currentStickText.text = manager.currentSticks.ToString() + "/2";
    }
}
