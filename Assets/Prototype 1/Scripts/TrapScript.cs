using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public float shootTime;
    public float shootInterval = 2f;
    public GameObject arrow;
    public Vector3 arrowPosition = new Vector3(-1.5f,0,0);
    private void Update()
    {
        shootTime -= Time.deltaTime;

        if (shootTime <= 0)
        {
            ShootArrow();
            shootTime = shootInterval;
        }
    }
    public void ShootArrow()
    {
        Vector3 spawnPos = transform.position + transform.up * 0.75f; 
        Quaternion rot = transform.rotation * Quaternion.Euler(0, 0, -90);
        Instantiate(arrow, spawnPos, rot);
    }
}
