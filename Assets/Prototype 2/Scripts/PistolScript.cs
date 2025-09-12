using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int ammo;
    public float fireRate;
    public bool bulletShot;

    private pistolBullet bullet;
    void Start()
    {
        bullet = FindAnyObjectByType<pistolBullet>();
    }

    void Update()
    {
        
    }
    public void shootBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
