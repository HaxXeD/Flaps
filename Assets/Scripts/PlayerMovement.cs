using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public event System.Action OnInvisible,OffInvisible;
    GameObject magnetBox;
    bool isInvisible = false;
    public bool ReturnIsInvisible(){
        return isInvisible;
    }
    [SerializeField] float impulse = 10f;
    // [SerializeField] Button jumpButton = null;
    Rigidbody2D rb;
    Animator animator;

    bool CanFly = false;

    private void Awake()
    {
        magnetBox = transform.GetChild(0).gameObject;
        magnetBox.SetActive(false);
        CanFly = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        FindObjectOfType<FillScript>().onFuelEmpty += DisableRigidbody;
        FindObjectOfType<Spawner>().OnQuestionEnable += DisableCouroutine;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(1f,2f));
    }

    private void DisableCouroutine()
    {
        StopCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(5f,2f));
    }

    private void DisableRigidbody()
    {
        CanFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        //enable player controller
        PlayerController();
    }

    public void PlayerController()
    {
        if(CanFly){
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.up * impulse;
                animator.SetBool("impulse", true);

            }
            animator.SetFloat("yVelocity", rb.velocity.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //fuel check
        // if(collision.tag == "fuel")
        // {
        //     fill.GetComponent<Image>().fillAmount = 1f;
        // }
        if (collision.CompareTag("timer"))
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            // StartCoroutine(PowerUpTime(5f));
            // PowerUpTime(5f,2f);
            StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(5f,2f));
        }

        if(collision.CompareTag("invisible")){
            // Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            // tmp.a = .5f;
            // gameObject.GetComponent<SpriteRenderer>().color = tmp;
           StartCoroutine(FadeTo(.2f,1f,10f));
        }

        if(collision.CompareTag("magnet")){
            StartCoroutine(MagnetEnableDisable(10));
        }
    }
    IEnumerator MagnetEnableDisable(float time){
        magnetBox.SetActive(true);
        yield return new WaitForSeconds(time);
        magnetBox.SetActive(false);
    }

    
    IEnumerator FadeTo(float aValue, float aTime, float bTime)
    {
        
        OnInvisible?.Invoke();
        // col2D.enabled = false;
        // col2D.isTrigger = true;
        // isInvisible = true;
         float alpha = transform.GetComponent<SpriteRenderer>().color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             transform.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
         yield return new WaitForSecondsRealtime(bTime);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(aValue, 1, t));
             transform.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
         OffInvisible?.Invoke();
        // isInvisible = false;
        // col2D.enabled = true;
        // col2D.isTrigger = true;
     }
 
    
    private void OnCollisionEnter2D(Collision2D collision)    {

        //enemy collision detection
        if (collision.gameObject.tag == "enemy")
        {
            print("hello");
            collision.collider.isTrigger = true;
            DisableRigidbody();
        }




    }

    // IEnumerator PowerUpTime(float time,float timertime)
    // {
    //     yield return new WaitForSecondsRealtime(time);
    //     StartCoroutine(ScaleTime(Time.timeScale, 1f, timertime));
    // }

    // IEnumerator ScaleTime(float start, float end, float time)
    // {
    //     float lastTime = Time.realtimeSinceStartup;
    //     float timer = 0.0f;

    //     while (timer < time)
    //     {
    //         Time.timeScale = Mathf.Lerp(start, end, timer / time);
    //         Time.fixedDeltaTime = Time.timeScale * 0.02f;
    //         timer += (Time.realtimeSinceStartup - lastTime);
    //         lastTime = Time.realtimeSinceStartup;
    //         yield return null;
    //     }
    //     Time.timeScale = end;
    //     Time.fixedDeltaTime = Time.timeScale * 0.02f;
    // }

}
