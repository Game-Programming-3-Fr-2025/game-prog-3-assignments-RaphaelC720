using System.Drawing;
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
            Vector2 playerPos = transform.position;

            Vector2 rawPull = player.currentMouseWorld - playerPos;
            Vector2 clampedEnd = playerPos + rawPull;

            line.enabled = true;
            line.positionCount = 25;
            float stepTime = 0.05f;
            Vector2 launchDir = (playerPos - clampedEnd).normalized;
            float a = Mathf.Clamp01(player.playerRawMag / player.maxPullDistance);
            
            float launchSpeed = Mathf.Lerp(player.minImpulse, player.maxImpulse, a);
            Vector2 velocity = launchDir * launchSpeed;            
            //if (player.playerRawMag > 2.5)
            for (int i = 0; i < 25; i++)
            {
                float intervalTime = i * stepTime;
                //calculation
                Vector2 point2D = playerPos + (velocity * intervalTime) + (0.5f * Physics2D.gravity * (intervalTime*intervalTime));
                
                Vector3 point3D = new Vector3(point2D.x, point2D.y, 0f);
                line.SetPosition(i, point3D);
            }

            arrow.gameObject.SetActive(true);

            float t = Mathf.Clamp01(player.playerRawMag / 10);
            arrow.localScale = Vector3.one * Mathf.Lerp(1f, 3.5f, t);
            
            arrow.position = playerPos + launchDir * Mathf.Lerp(0.5f, 2f, t);

            float angle = Mathf.Atan2(launchDir.y, launchDir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            line.enabled = false;
            arrow.gameObject.SetActive(false);
        }
    }
}
