using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public float astroidSpeed;
    private Rigidbody2D astroidRB;
    void Start()
    {
        astroidRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
