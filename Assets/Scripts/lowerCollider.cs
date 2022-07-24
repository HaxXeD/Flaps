using UnityEngine;

public class lowerCollider : MonoBehaviour
{
    public event System.Action OnPlayerDeath;
    [SerializeField] GameObject player;  

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject == player)
        {
            OnPlayerDeath?.Invoke();
            Destroy(player);
        }
    }
}
