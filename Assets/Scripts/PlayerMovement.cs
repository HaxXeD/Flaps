using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float impulse = 10f;
    Rigidbody2D rb;
    Animator animator;
    FillScript fill;
    bool isActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fill = FindObjectOfType<FillScript>();
    }

    // Update is called once per frame
    void Update()
    {
        fill.onFuelEmpty += DisableRigidBody;

        //enable player controller
        PlayerController();
    }

    private void PlayerController()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!isActive)
        {
            rb.velocity = Vector2.up * impulse;
            animator.SetBool("impulse", true);

        }
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void DisableRigidBody()
    {
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //fuel check
        if(collision.tag == "fuel")
        {
            fill.GetComponent<Image>().fillAmount = 1f;
        }
        if (collision.CompareTag("timer"))
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            StartCoroutine(PowerUpTime(5f));
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)    {

        //enemy collision detection
        if (collision.gameObject.tag == "enemy")
        {
            isActive = true;
            collision.collider.isTrigger = true;
        }
    }

    IEnumerator PowerUpTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(ScaleTime(Time.timeScale, 1f, 2f));
    }

    IEnumerator ScaleTime(float start, float end, float time)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }
        Time.timeScale = end;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

}
