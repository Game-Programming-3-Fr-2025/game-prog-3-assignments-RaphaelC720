using System;
using System.Collections;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public static event Action<bool> flagTouched;
    [SerializeField] private Slime_Script player;

    public GameObject QTEPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return; 

        if (CompareTag("StartFlag"))
        {
            Debug.Log("[EndScript] Start flag touched");
            flagTouched?.Invoke(true);
            player.moveSpeed = 7.5f;
            if (FindAnyObjectByType<SpeedScript>() == null)
                Instantiate(player.speedPowerup, player.PowerupPos, Quaternion.identity);
        }
        else if (CompareTag("EndFlag"))
        {
            Debug.Log("[EndScript] End flag touched");
            player.inQTE = true;
            QTEPanel.SetActive(true);
            StartCoroutine(DoQTE(QTEtype.Arrows));
        }
    }

    IEnumerator DoQTE(QTEtype type)
    {
        QTEresult result = QTEresult.Perfect;
        if (type == QTEtype.Arrows)
        {
            arrowQTE aQTE = FindAnyObjectByType<arrowQTE>();
            yield return StartCoroutine(aQTE.StartQTE((r) => result = r));
        }

        if (result == QTEresult.Miss)
        {
            StartCoroutine(DoQTE(QTEtype.Arrows));
        }
        else if (result == QTEresult.Perfect)
        {
            player.inQTE = false;
            QTEPanel.SetActive(false);
            flagTouched?.Invoke(false);
            player.die();
        }
    }
}
public enum QTEtype
{
    Arrows = 0,
}
public enum QTEresult
{
    Miss = 0,
    Perfect = 1
}