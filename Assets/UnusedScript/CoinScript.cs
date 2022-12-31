using UnityEngine;

public class CoinScript: MonoBehaviour
{
    ScoreAndCoin coin;
    ListAudio listAudio;
    ObjectMovementScript movementScript;
    CoinBehavior coinBehavior;
    [SerializeField] GameObject CoinParticle;
    // PlayerMovement playerMovement;

    public bool isMagnetEnabled = false;
    private void Awake() => coin = FindObjectOfType<ScoreAndCoin>();

    void Start()
    {
        // playerMovement = FindObjectOfType<PlayerMovement>();
        movementScript = GetComponent<ObjectMovementScript>();
        coinBehavior = GetComponent<CoinBehavior>();
        listAudio = FindObjectOfType<ListAudio>();
        coinBehavior.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            coin.AddCoin();
            GameObject coinParticleInstance = Instantiate(CoinParticle,collision.transform.position,Quaternion.identity);
            listAudio.PlayAudioWithOneShot(2);
            Destroy(gameObject);
        }

        if (collision.CompareTag("coincollector"))
        {
            movementScript.enabled = false;
            coinBehavior.enabled = true;
        }
    }
}
