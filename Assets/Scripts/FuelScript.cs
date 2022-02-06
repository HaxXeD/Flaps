using UnityEngine;

public class FuelScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Destroy(gameObject);
    }
}
