using UnityEngine;
using UnityEngine.Events;

public class TimeScaleController : MonoBehaviour
{

    //create unity event for pause and unpause
    [HideInInspector] public UnityEvent OnPause,OnUnpause = new UnityEvent();
    public event System.Action OnQuestionStartTimeScaleSetToZero,
                                OnPauseStopAllPowerUps,
                                OnUnpauseEnableControls;

    [SerializeField] EaseUp easeUp;
    [SerializeField] QuestionSpawner questionSpawner;
    [SerializeField] PlayerCollision playerCollision;
    void Start(){
        //Ease in when the game starts
        ManipulateTimeScale(0);
        StartCoroutine(easeUp.ScaleTime(Time.timeScale,1,2f));
        questionSpawner.OnQuestionPanelActive += OnQuestionStarted;
        questionSpawner.OnQuestionPanelInactive += OnPauseInvoked;
        playerCollision.OnPowerUp += ManipulateTimeScale;
        OnPause.AddListener(OnPauseInvoked);
        OnUnpause.AddListener(OnUnpauseInvoked);
    }

    //Set time scale 
    public void ManipulateTimeScale(float timeScale){
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    //Set time scale to zero when question panel is active
    void OnQuestionStarted(){
        //Stop all power up coroutines
        OnQuestionStartTimeScaleSetToZero?.Invoke();
        ManipulateTimeScale(0);
    }

    //Set time scale to zero when game is paused
    //there is still an issue to be fixed for this, as when pausing
    //player will first pass an input which will make them jump, 
    //which then trancends when unpausing
    void OnPauseInvoked(){
        //stop all power up corouitnes
        OnPauseStopAllPowerUps?.Invoke();
        ManipulateTimeScale(0);
    }

    //Ease timescale to 1 when game is unpaused
    private void OnUnpauseInvoked()
    {
        //Enable movement after some time
        OnUnpauseEnableControls?.Invoke();
        ManipulateTimeScale(0);
        StartCoroutine(easeUp.ScaleTime(Time.timeScale,1,2f));
    }
}
