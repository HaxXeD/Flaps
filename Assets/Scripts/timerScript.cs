using System.Collections;
using UnityEngine;

public class timerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Destroy(gameObject);
    }

}
