using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("player")) Destroy(gameObject);
    }
}