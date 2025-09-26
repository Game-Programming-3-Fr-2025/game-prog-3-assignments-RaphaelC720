using UnityEngine;

public class CameraScript2 : MonoBehaviour
{
    private Vector3 target;
    public GameObject player;
    public GameObject crosshair;
    public GameObject rockPrefab;

    public float rockSpeed;
    public float offSet = 0.5f;
    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (player != null)
        {
            target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            crosshair.transform.position = new Vector2(target.x, target.y);

            Vector3 difference = target - player.transform.position;

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (Input.GetMouseButtonDown(0))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                throwRock(direction, rotationZ);
            }
        }
    }
    void throwRock(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(rockPrefab);
        b.transform.position = (Vector2)player.transform.position + direction * offSet;
        b.transform.rotation = Quaternion.Euler(0f,0f, rotationZ);
        b.GetComponent<Rigidbody2D>().linearVelocity = direction * rockSpeed;
    }
}
