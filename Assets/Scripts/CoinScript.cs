using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript: MonoBehaviour
{
    ScoreAndCoin coin;
    private void Awake() => coin = FindObjectOfType<ScoreAndCoin>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            coin.AddCoin();
            Destroy(gameObject);
        }
    }
}
