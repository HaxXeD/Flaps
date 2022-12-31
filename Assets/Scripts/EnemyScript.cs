using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] CapsuleCollider2D col2D;
    
    PlayerCollision playerCollision;


    // Start is called before the first frame update
    void Start()
    {
        playerCollision = FindObjectOfType<PlayerCollision>();
        if(playerCollision==null)return;
        //when shield is active disable collider
        playerCollision.OnShieldActive += DisableCollider;
        //when shield is inactive enable collider
        playerCollision.OnShieldInactive += EnableCollider;

        //negate the shield activation with collision
        col2D.enabled = !playerCollision.ReturnShield();
    }

    private void DisableCollider() => col2D.enabled = false;

    private void EnableCollider() => col2D.enabled = true;


    //unsubscribe form all events on destory
    void OnDestroy(){
        if(playerCollision==null)return;
        playerCollision.OnShieldActive -= DisableCollider;
        playerCollision.OnShieldInactive -= EnableCollider;
    }
}


