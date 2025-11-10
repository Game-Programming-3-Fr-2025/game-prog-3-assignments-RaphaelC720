using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EndScript;

public class arrowQTE : MonoBehaviour
{
    public List<KeyCode> sequence = new List<KeyCode>();
    public int numOfArrows;
    public GameObject arrowPrefab;
    public Transform arrowUI;
    private List<Image> arrowImages = new List<Image>();
    public IEnumerator StartQTE(System.Action <QTEresult> callback)
    {
        sequence.Clear();
        for (int i = 0; i < numOfArrows; i++)
        {
            KeyCode arrow = GetRandomArrow();
            sequence.Add(arrow);
        }

        for (int i = 0; i < sequence.Count; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowUI);
            arrow.SetActive(true);

            Image Img = arrow.GetComponent<Image>();
            RectTransform rt = arrow.GetComponent<RectTransform>();

            switch (sequence[i])
            {
                case KeyCode.UpArrow: rt.rotation = Quaternion.Euler(0, 0, 0); break;
                case KeyCode.RightArrow: rt.rotation = Quaternion.Euler(0, 0, -90); break;
                case KeyCode.DownArrow: rt.rotation = Quaternion.Euler(0, 0, 180); break;
                case KeyCode.LeftArrow: rt.rotation = Quaternion.Euler(0, 0, 90); break;
            }
            arrowImages.Add(Img);
        }

        int correctInputs = 0;
        int currentIndex = 0;

        while (currentIndex < sequence.Count)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(sequence[currentIndex]))
                {
                    correctInputs++;
                    arrowImages[currentIndex].color = Color.green;
                }
                else if (Input.anyKeyDown)
                {
                    arrowImages[currentIndex].color = Color.red;
                }
                currentIndex++;
            }
            yield return null;
        }

        if (correctInputs == numOfArrows)
            callback(QTEresult.Perfect);
        else
            callback(QTEresult.Miss);

        foreach (Transform child in arrowUI)
            Destroy(child.gameObject);
        arrowImages.Clear();
    }

    KeyCode GetRandomArrow()
    {
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0: return KeyCode.LeftArrow;
            case 1: return KeyCode.RightArrow;
            case 2: return KeyCode.UpArrow;
            case 3: return KeyCode.DownArrow;
        }
        return KeyCode.Space;
    }
}

