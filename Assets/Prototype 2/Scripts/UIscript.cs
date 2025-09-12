using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    public Image rocketFillBar;
    
    public rocketScript rocket;
    void Start()
    {
        rocket = FindAnyObjectByType<rocketScript>();
    }

    void Update()
    {
        float HealthPercent = Mathf.Clamp01(rocket.currentHealth / rocket.maxHealth);
        rocketFillBar.fillAmount = HealthPercent;
    }
}
