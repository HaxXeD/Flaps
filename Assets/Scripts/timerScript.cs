using UnityEngine;

public class timerScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    { 
            // FindObjectOfType<PlayerMovement>().TimerOn();
            Destroy(gameObject);
    }

}
