using System;
using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Cache sfx and postprocessing
    ListAudio listAudio;
    ChangePostProcessing changePostProcessing;


    //Serialize rigidbody
    [SerializeField]Rigidbody2D rb;


    //serialize collider
    [SerializeField] CapsuleCollider2D capsuleCollider2D;


    //serialize quesiton spawner
    [SerializeField]QuestionSpawner questionSpawner;


    //serialize power magnet and shield gameobject<sprite>
    [SerializeField]GameObject magnetBox,
                                shield;

    //serialize booster particle
    [SerializeField] ParticleSystem boosterStreaks;

    //serialize timescale controller
    [SerializeField] TimeScaleController timeScaleController;

    //Cache slow down span and ease up coroutines
    Coroutine C_SlowDownSpan,
            C_SlowDownEaseUp,

            //cache booster span and ease up and end coroutines 
            C_BoosterSpan,
            C_BoosterEaseUp,
            C_BoosterEnd,

            //cache magnet and invisible coroutine
            C_Magnet,
            C_Invisible;

    //Cache slow down enumerator
    IEnumerator I_SlowDownSpan,
                I_boosterSpan;

    //event to set canfly boolean           
    public event Action<bool> SetCanFly;

    //events to control powerups
    public event Action OnBoosterEnd, 
                        OnSlowDownEnd, 
                        OnMagnetActive,
                        OnMagnetInactive,
                        OnShieldActive,
                        OnShieldInactive;

    //event to pass the timescale when colliding with powerups
    public event Action<float> OnPowerUp;

    //Referencing Booleans
    bool isBoosterEnabled = false,
        isSlowDownEnabled = false,
        isShieldActive = false;


    //return shield boolean value to enable and disable enemy plane collision
    public bool ReturnShield() => isShieldActive;
    void Awake(){ 
        listAudio = FindObjectOfType<ListAudio>();
        changePostProcessing = FindObjectOfType<ChangePostProcessing>();

        //time scale event subscription is used due to subscription order of OnQuestionStart event
        //where StopPowerCoroutine is subribed first before setting the time scale to zero, 
        //this can be coded better but for now this will do.
        timeScaleController.OnQuestionStartTimeScaleSetToZero += StopPowerCoroutines;
        timeScaleController.OnPauseStopAllPowerUps += StopPowerCoroutines;
        timeScaleController.OnUnpauseEnableControls +=SetCanFlyTrueWithDelay;

        //When question is shown to the players
        questionSpawner.OnQuestionPanelActive += StopPowerCoroutines;

        //When the question is answerd or not, player control is enabled again
        questionSpawner.OnQuestionPanelInactive += EnableGravityAndCollision;
        questionSpawner.OnQuestionPanelInactive += SetSlowDownFalse;

        //When the booster time is finished
        OnBoosterEnd += EnableGravityAndCollision;

        //When the slow down time is finished
        OnSlowDownEnd += SetSlowDownFalse;

        //When shield in active
        OnShieldActive += SetShieldActive;

        //When shield in inactive
        OnShieldInactive += SetShieldInactive;
    }

    private void SetShieldActive() => isShieldActive = true;

    private void SetShieldInactive() => isShieldActive = false;

    private void SetCanFlyTrueWithDelay() => Invoke(nameof(SetCanFlyTrue),.1f);
    private void SetCanFlyTrue() => SetCanFly.Invoke(true);

    private void SetSlowDownFalse()
    {
        //set slow down boolean to false
        isSlowDownEnabled = false;

        //reset slow down post prosessing
        changePostProcessing.OnSlowDownEnd.Invoke();
        print("Timer Has Ended"); 
    }

    private void StopPowerCoroutines()
    {
        //firstly check the coroutines to be null, if not stop them
        if(C_SlowDownEaseUp!=null)StopCoroutine(C_SlowDownEaseUp);
        if(C_SlowDownSpan!=null)StopCoroutine(C_SlowDownSpan);
        if(C_BoosterEaseUp!=null)StopCoroutine(C_BoosterEaseUp);
        if(C_BoosterSpan!=null)StopCoroutine(C_BoosterSpan);
        if(C_BoosterEnd!=null)StopCoroutine(C_BoosterEnd);
        SetCanFly.Invoke(false);
    }

    //reset controls and booster 
    private void EnableGravityAndCollision()
    {
        //reset booster post processing
        changePostProcessing.OnBoosterEnd.Invoke();

        //stop booster particle effect
        boosterStreaks.Stop();

        //set booster boolean to false
        isBoosterEnabled = false;

        //this method is delayed due to players
        //clicking an answer and passing an input whilst doing so
        //so delaying the method disables that 
        SetCanFlyTrueWithDelay();
        Debug.Log("boosterStatus " + isBoosterEnabled);

        rb.gravityScale = 3f;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("slowdown"))
        {
            //this guard clause checks if the booster is enabled
            //if so, the slow down powerup is not activated
            if(isBoosterEnabled) return;

            isSlowDownEnabled = true;
            listAudio.PlayAudioWithOneShot(4);
            
            //start slow down post processing
            changePostProcessing.OnSlowDownStart.Invoke();

            //set timesclae to half 
            OnPowerUp.Invoke(.5f);
            
            //cache slow down ease up enumerator
            I_SlowDownSpan = StartEaseUpSlowDown(5,2f,OnSlowDownEnd);

            //star slow down span coroutine
            C_SlowDownSpan = StartCoroutine(I_SlowDownSpan);
        }

        else if(collision.CompareTag("booster"))
        {
            //stop previously started booster coroutines, 
            //to restart if player recollides with new booster powerup
            if(C_BoosterEaseUp!=null)StopCoroutine(C_BoosterEaseUp);
            else if(C_BoosterSpan!=null)StopCoroutine(C_BoosterSpan);

            //set time scale to 3
            OnPowerUp.Invoke(3f);

            //disable flying
            SetCanFly.Invoke(false);

            //start booster post processing
            changePostProcessing.OnBoosterStart.Invoke();

            //start booster particle effect
            boosterStreaks.Play();

            //set booster boolean to true
            isBoosterEnabled = true;

            //set rigidbody velocity and gravity to zero, and body type to kinematic
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;

            //cache to booster span enumerator
            I_boosterSpan = StartEaseUpBooster(5,5,OnBoosterEnd);

            //start the booster span coroutine
            C_BoosterSpan = StartCoroutine(I_boosterSpan);
            Debug.Log("boosterStatus " + isBoosterEnabled);
        }

        else if(collision.CompareTag("invisible"))
        {
            //stop previously started invisible coroutines, 
            //to restart if player recollides with new invisible powerup
            if(C_Invisible!=null)StopCoroutine(C_Invisible);
            listAudio.PlayAudioWithOneShot(5);

            //set the alpha of the invisible sprite to 0
            Color newColor = new Color(1, 1, 1, 0);
            shield.GetComponent<SpriteRenderer>().color = newColor;

            //start the coroutine for fade effect of invisible power up
            C_Invisible = StartCoroutine(SpriteFade(.8f,1f,10f,shield,OnShieldActive,OnShieldInactive));
        }

        else if(collision.CompareTag("magnet"))
        {
            //stop previously started magnet coroutines, 
            //to restart if player recollides with new magnet powerup
            if(C_Magnet!=null)StopCoroutine(C_Magnet);
            listAudio.PlayAudioWithOneShot(3);

            //set the alpha of the magnet sprite to 0
            Color newColor = new Color(1, 1, 1, 0);
            magnetBox.GetComponent<SpriteRenderer>().color = newColor;

            //start the coroutine for fade effect of magnet power up
            C_Magnet = StartCoroutine(SpriteFade(.5f,1f,10f,magnetBox,OnMagnetActive,OnMagnetInactive));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" )
        {
            StopPowerCoroutines();
            listAudio.PlayAudioWithOneShot(19);
            collision.collider.isTrigger = true;
            SetCanFly.Invoke(false);
        }
    }

    //enumerator for sprite fade effect, 
    //aValue -> final alpha value
    //aTime -> effect time <speed>
    //bTime -> wait time
    //_InvokeEventStart -> starting event
    //_InvokeEventEnd -> ending event
    IEnumerator SpriteFade(float aValue, float aTime, float bTime, GameObject gameObject,Action _InvokeEventStart,Action _InvokeEventEnd)
    {
        _InvokeEventStart?.Invoke();
        
        gameObject.SetActive(true);

        //this has been set to 0
        float alpha = gameObject.GetComponent<SpriteRenderer>().color.a;

        //set to final value ..aValue.. in time ..aTime.. 
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        //wait time
        yield return new WaitForSecondsRealtime(bTime);

        //set alpha to 0 
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(aValue, 0, t));
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        gameObject.SetActive(false);
        _InvokeEventEnd?.Invoke();
    }


    //the StartEaseUpBooster and StartEaseUpSlowDown have different enumerators
    //even thought they provide the same functionality
    //just so the individual coroutines could be cached

    //time -> wait time
    //timertime -> ease up time
    //ievent -> event invoked at the end of coroutine

    #region 
    public IEnumerator StartEaseUpBooster(float time, float timertime, Action ievent)
    { 
        print("Ease Up Called For Booster.");
        yield return new WaitForSecondsRealtime(time);
        print("Scale Time for Booster Called.");
        C_BoosterEaseUp = StartCoroutine(ScaleTime(Time.timeScale,1f,timertime,ievent));   
    }

    public IEnumerator StartEaseUpSlowDown(float time, float timertime, Action ievent)
    { 
        print("Ease Up Called For Timer");
        yield return new WaitForSecondsRealtime(time);
        print("Scale Time for Timer Called");
        C_SlowDownEaseUp = StartCoroutine(ScaleTime(Time.timeScale,1f,timertime,ievent));   
    }
    #endregion

    //this enumerator scales the timescale
    //start -> starting timesclae
    //end -> ending timescale
    //time -> time taken
    //ivokeEvent -> from EaseUp coroutine above
    public IEnumerator ScaleTime(float start, float end, float time, Action invokeEvent)
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
        invokeEvent?.Invoke();
    }

    //Unsubscribe all event on destroy
    void OnDestroy(){
    //    timeScaleController.OnQuestionStartTimeScaleSetToZero -= StopPowerCoroutines;
        questionSpawner.OnQuestionPanelInactive -= EnableGravityAndCollision;
        OnBoosterEnd -= EnableGravityAndCollision;
        OnSlowDownEnd -= SetSlowDownFalse;
        questionSpawner.OnQuestionPanelActive -= StopPowerCoroutines;
        OnShieldActive -= SetShieldActive;
        OnShieldInactive -= SetShieldInactive;
    }
}


