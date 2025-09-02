using PrototypeOne;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class GameManager : MonoBehaviour
{
    public int currentSticks = 0;
    public GameObject CampfirePanel;
    public GameObject campfireLight;
    public GameObject campfireText;
    public GameObject currentSticksPanel;
    public GameObject arrow;

    public bool CampFireActive = false;

    public PlayerScript player;
    private void Update()
    {
        if (currentSticks == 2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MakeFire();
            }
        }
    }

    public void MakeFire()
    {
        campfireLight.SetActive(true);
        currentSticks = 0;
        CampfirePanel.SetActive(false);
        campfireText.SetActive(false);
        currentSticksPanel.SetActive(false);
        arrow.SetActive(false);
        CampFireActive = true;

        player.mylight.intensity = 3;
        player.mylight.falloffIntensity = 10;
        player.mylight.transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
    }
}
