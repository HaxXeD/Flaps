using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    CapsuleCollider2D col2D;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        col2D = GetComponent<CapsuleCollider2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        if(playerMovement!=null){
            playerMovement.OnInvisible += DisableCollider;
            playerMovement.OffInvisible += EnableCollider;
        }
    }

    private void DisableCollider()
    {
        col2D.enabled = false;
    }

    private void EnableCollider()
    {
        col2D.enabled = true;
    }

    void OnDisable(){
        if(playerMovement!=null){
            playerMovement.OnInvisible -= DisableCollider;
            playerMovement.OffInvisible -= EnableCollider;

        }
    }
}


