using UnityEngine;
public class PointerScript : MonoBehaviour
{
    public Transform Target;
    public float HideDistance;
    void Update()
    {
        var dir = Target.position - transform.position; 
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
