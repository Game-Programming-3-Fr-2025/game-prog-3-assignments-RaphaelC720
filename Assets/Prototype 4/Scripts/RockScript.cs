using UnityEngine;

public class RockScript : MonoBehaviour
{
    private myPlayerScript playerScript;
    private ScorpionScript enemy1;
    public void Start()
    {
        playerScript = FindAnyObjectByType<myPlayerScript>();
        GameObject scorpion = GameObject.FindGameObjectWithTag("enemy");
        if (scorpion != null)
        {
            enemy1 = scorpion.GetComponent<ScorpionScript>();
        }
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        ScorpionScript scorpion = other.GetComponent<ScorpionScript>()
                           ?? other.GetComponentInParent<ScorpionScript>()
                           ?? other.GetComponentInChildren<ScorpionScript>();

        if (other.CompareTag("enemy"))
        {
            if (playerScript != null)
            {
                scorpion.takedmg(playerScript.playerDmg);
                Destroy(gameObject);
                return;
            }
        }
        else if (other.CompareTag("wall") || other.CompareTag("door"))
        {
            Destroy(gameObject);
        }
    }
    public void attack(int dmg)
    {
        enemy1.takedmg(dmg);
    }
}
