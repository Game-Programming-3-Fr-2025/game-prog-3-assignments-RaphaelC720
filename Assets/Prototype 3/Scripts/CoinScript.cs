using System.Collections;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Sprite[] collectSprites;
    public float frameRate = 0.1f;
    private SpriteRenderer coinSR;    
    public ParticleSystem coinPS;
    void Start()
    {
        coinPS = GetComponent<ParticleSystem>();
        coinSR = GetComponent<SpriteRenderer>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            coinPS.Play();
            StartCoroutine(PlayAnimation());
        }
    }
    private IEnumerator PlayAnimation()
    {
        for (int i = 0; i < collectSprites.Length; i++)
        {
            coinSR.sprite = collectSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        Destroy(gameObject);
    }

}
