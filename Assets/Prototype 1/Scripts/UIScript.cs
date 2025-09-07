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

        float healthPercent = Mathf.Clamp01(player.currentHealth / player.maxHealth);
        fillbar.fillAmount = healthPercent;

        if (manager.CampFireActive) return;

        if (CampfiretxtPanel.activeSelf)
        {
            return;
        }
        else if(manager.currentSticks >= 2)
        {
            TurnOnTextToCampfire();
        }
        
    }
    public void UpdateStickText()
    {
        currentStickText.text = manager.currentSticks.ToString() + "/2";
    }

    public void TurnOnTextToCampfire()
    {
        CampfiretxtPanel.SetActive(true);
        Arrow.SetActive(true);
    }
}
