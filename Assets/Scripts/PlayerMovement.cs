using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    //caching sfx 
    ListAudio listAudio;

    //serializing impulse force, rigidbody and animator
    [SerializeField]float impulse = 10f;
    [SerializeField]Rigidbody2D rb;

    //animator is currently not used, this will be used to add jump animation
    [SerializeField]Animator animator;

    //to control player inputs. not let players spam the jump
    bool CanFly = true;

    private void Start()
    {
        listAudio = FindObjectOfType<ListAudio>();

        //to disable player input when fuel is empty
        FindObjectOfType<FillScript>().OnFuelEmpty += UpdateCanFlyBool;

        //to disable player input when it collides with enemy plane
        GetComponent<PlayerCollision>().SetCanFly += UpdateCanFlyBool;
    }

    private void UpdateCanFlyBool(bool _canFly) => CanFly =  _canFly;

    void Update() => PlayerController();
    public void PlayerController()
    {
        if(CanFly){
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
            {
                listAudio.PlayAudioWithOneShot(6);
                rb.velocity = Vector2.up * impulse;
            }
        }
    }
}
