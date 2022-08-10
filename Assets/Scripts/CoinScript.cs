using UnityEngine;

public class CoinScript: MonoBehaviour
{
    ScoreAndCoin coin;
    ListAudio listAudio;
    ObjectMovementScript movementScript;
    MoveTowardsPlayer moveTowardsPlayer;
    PlayerMovement playerMovement;

    public bool isMagnetEnabled = false;
    private void Awake() => coin = FindObjectOfType<ScoreAndCoin>();

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        movementScript = GetComponent<ObjectMovementScript>();
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        listAudio = FindObjectOfType<ListAudio>();
        moveTowardsPlayer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            coin.AddCoin();
            listAudio.PlayAudioWithOneShot(2);
            Destroy(gameObject);
        }

        if (collision.CompareTag("coincollector"))
        {
            movementScript.enabled = false;
            moveTowardsPlayer.enabled = true;
        }
    }
}
