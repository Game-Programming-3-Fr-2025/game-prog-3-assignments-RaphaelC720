using UnityEngine;

public class PullUI : MonoBehaviour
{
    public PlayerScript3 player;
    public LineRenderer line;
    public Transform arrow;

    void Update()
    {
        if (player.state == PlayerStates.Launching)
        {
            Vector3 playerPos = transform.position;

            Vector3 rawPull = player.currentMouseWorld - playerPos;
            float rawMag = rawPull.magnitude;
            float usedMag = Mathf.Min(rawMag, player.maxPullDistance);
            Vector3 clampedEnd = playerPos + rawPull.normalized * usedMag;

            line.enabled = true;
            line.positionCount = 2;
            line.SetPosition(0, playerPos);
            line.SetPosition(1, clampedEnd);

            Vector3 launchDir = (playerPos - clampedEnd).normalized;
            arrow.gameObject.SetActive(true);

            float t = Mathf.Clamp01(rawMag / player.maxPullDistance);
            arrow.localScale = Vector3.one * Mathf.Lerp(0.5f, 2.5f, t);
            arrow.position = playerPos + launchDir * Mathf.Lerp(0.5f, 2f, t);

            float angle = Mathf.Atan2(launchDir.y, launchDir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, angle);

        }
        else
        {
            line.enabled = false;
            arrow.gameObject.SetActive(false);
        }

        float lineLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
        line.material.mainTextureScale = new Vector2(lineLength, 1f);
    }

}
