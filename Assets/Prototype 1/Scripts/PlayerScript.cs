using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PrototypeOne
{
    public class PlayerScript : MonoBehaviour
    {
        private Rigidbody2D myRB;
        public float moveSpeed = 5f;
        private float moveInput;

        public float currentHealth;
        public float maxHealth;

        public Light2D mylight;

        void Start()
        {
            myRB = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
        }
        void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 newPosition = myRB.position + input.normalized * moveSpeed * Time.fixedDeltaTime;
            myRB.MovePosition(newPosition);
        }
    }
}