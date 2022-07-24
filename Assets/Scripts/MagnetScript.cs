using UnityEngine;

public class MagnetScript : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // StartCoroutine(FindObjectOfType<PlayerMovement>().MagnetEnableDisable(10f));
            Destroy(gameObject);
        }
    }

