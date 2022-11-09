using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    public UnityEvent OnUnpause = new UnityEvent();
    public event System.Action OnInvisible,OffInvisible;
    event System.Action OnRocket;
    [SerializeField]GameObject magnetBox,shield;
    bool isInvisible = false;
    public bool ReturnIsInvisible(){
        return isInvisible;
    }
    [SerializeField] float impulse = 10f;
    // [SerializeField] Button jumpButton = null;
    Rigidbody2D rb;
    Animator animator;
    ListAudio listAudio;

    bool CanFly = false;

    private void Awake()
    {
        magnetBox = transform.GetChild(0).gameObject;
        shield = transform.GetChild(1).gameObject;
        magnetBox.SetActive(false);
        shield.SetActive(false);
        CanFly = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        listAudio = FindObjectOfType<ListAudio>();
        FindObjectOfType<FillScript>().onFuelEmpty += DisableRigidbody;
        FindObjectOfType<Spawner>().OnQuestionEnable += DisableCouroutine;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(.5f,2f));
        FindObjectOfType<SettingButtons>().OnPause+=StopEaseUp;
        FindObjectOfType<SettingButtons>().OnUnpause+=StartEaseUp;
        OnRocket+=DisableCouroutine;
    }

    void Start(){
        OnUnpause.AddListener(StartEaseUp);
    }

    private void DisableCouroutine()
    {
        StopCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(5f,2f));
    }

    private void DisableRigidbody()
    {
        CanFly = false;
    }

    private void StopEaseUp(){
        FindObjectOfType<EaseUp>().StopAllCoroutines();
        Time.timeScale = 0f;
    }

    public void StartEaseUp(){
        print("unpaused");
        StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(.5f,2f));

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
                listAudio.PlayAudioWithOneShot(6);
                rb.velocity = Vector2.up * impulse;
                // animator.SetBool("impulse", true);

            }
            // animator.SetFloat("yVelocity", rb.velocity.y);
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
            listAudio.PlayAudioWithOneShot(4);
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            // StartCoroutine(PowerUpTime(5f));
            // PowerUpTime(5f,2f);
            StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(5f,2f));
        }

        if(collision.CompareTag("invisible")){
            listAudio.PlayAudioWithOneShot(5);
            // Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            // tmp.a = .5f;
            // gameObject.GetComponent<SpriteRenderer>().color = tmp;
            shield.SetActive(true);
            Color newColor = new Color(1, 1, 1, 0);
            shield.GetComponent<SpriteRenderer>().color = newColor;
           StartCoroutine(FadeTo(.8f,1f,10f));
        }

        if(collision.CompareTag("magnet")){
            listAudio.PlayAudioWithOneShot(3);
            magnetBox.SetActive(true);
            Color newColor = new Color(1, 1, 1, 0);
            magnetBox.GetComponent<SpriteRenderer>().color = newColor;
            StartCoroutine(MagnetEnableDisable(.5f,1f,10f));
        }

        if(collision.CompareTag("rocket")){
            OnRocket?.Invoke();
            // rb.velocity = Vector2.up * impulse;
            Time.timeScale = 3f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            // rb.gravityScale = 0f;
            // CanFly = false;
            CanFly = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            OnInvisible?.Invoke();
            StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(5f,5f));
            StartCoroutine(PowerUpEnd(11f));
        }
    }

    IEnumerator PowerUpEnd(float waitTime){
        yield return new WaitForSecondsRealtime(waitTime);
        // rb.gravityScale = 3f;
        CanFly = true;
        rb.gravityScale = 3f;
        // rb.velocity = Vector2.one;
        rb.bodyType = RigidbodyType2D.Dynamic;
        // rb.velocity = Vector2.up * impulse;
        OffInvisible?.Invoke();
    }

    IEnumerator MagnetEnableDisable(float aValue, float aTime, float bTime){
        magnetBox.SetActive(true);
        float alpha = magnetBox.GetComponent<SpriteRenderer>().color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             magnetBox.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
         yield return new WaitForSecondsRealtime(bTime);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(aValue, 0, t));
             magnetBox.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
         magnetBox.SetActive(false);
    }

    
    IEnumerator FadeTo(float aValue, float aTime, float bTime)
    {
        
        OnInvisible?.Invoke();
        // col2D.enabled = false;
        // col2D.isTrigger = true;
        // isInvisible = true;
         float alpha = shield.GetComponent<SpriteRenderer>().color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             shield.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
         yield return new WaitForSecondsRealtime(bTime);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(aValue, 0, t));
             shield.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
         shield.SetActive(false);
         OffInvisible?.Invoke();
        // isInvisible = false;
        // col2D.enabled = true;
        // col2D.isTrigger = true;
     }
 
    
    private void OnCollisionEnter2D(Collision2D collision)    {

        //enemy collision detection
        if (collision.gameObject.tag == "enemy")
        {
            listAudio.PlayAudioWithOneShot(19);
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
