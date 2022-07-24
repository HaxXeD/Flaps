using UnityEngine;

public class FuelScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            // StartCoroutine(FindObjectOfType<PlayerMovement>().FadeTo(.2f,1f,10f));
            Destroy(gameObject);
    }
}
