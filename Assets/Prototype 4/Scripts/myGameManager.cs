using UnityEngine;

public class myGameManager : MonoBehaviour
{
    public static myGameManager Instance;

    public float myMaxHealth;
    public float mySpeed;
    public int myDamage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
