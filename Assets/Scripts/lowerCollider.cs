using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class lowerCollider : MonoBehaviour
{
    public event System.Action OnPlayerDeath;
    [SerializeField] GameObject player;  

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject.tag == "player")
        {
            OnPlayerDeath?.Invoke();
            Destroy(player.transform.parent.gameObject);
        }
    }

}
