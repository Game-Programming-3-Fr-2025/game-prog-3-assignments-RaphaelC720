using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace PrototypeOne
{
    public class PlayerScript : MonoBehaviour
    {
        private Rigidbody2D myRB;
        public float moveSpeed = 5f;
        private float moveInput;

        public float currentHealth;
        public float maxHealth;
        public bool died;

        public Light2D mylight;

        public GameManager GM;
        void Start()
        {
            myRB = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
            died = false;
        }
        void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 newPosition = myRB.position + input.normalized * moveSpeed * Time.fixedDeltaTime;
            myRB.MovePosition(newPosition);
            if (currentHealth <= 0)
            {
                die();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("arrow"))
            {
                currentHealth -= 10;
            }
        }

        void die()
        {
            if (GM.CampFireActive == false)
            {
                SceneManager.LoadScene("GameOne");
            }

            if (GM.CampFireActive == true)
            {
                currentHealth = 100;
                transform.position = new Vector3(-2.5f, 16.5f, 0);
            }
        }
    }
}