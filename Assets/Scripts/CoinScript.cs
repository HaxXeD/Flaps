using UnityEngine;

public class CoinScript: MonoBehaviour
{
    ScoreAndCoin coin;
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
        moveTowardsPlayer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            coin.AddCoin();
            Destroy(gameObject);
        }

        if (collision.CompareTag("coincollector")){
            movementScript.enabled = false;
            moveTowardsPlayer.enabled = true;
        }
    }
}
