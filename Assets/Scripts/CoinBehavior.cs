using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {   
        if(Player!=null){
            transform.position = Vector2.MoveTowards(transform.position,Player.transform.position,17*Time.deltaTime);
        }
    }
}
