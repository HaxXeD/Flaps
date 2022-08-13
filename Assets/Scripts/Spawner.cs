using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{

    public event System.Action OnQuestionEnable;
    [SerializeField] JsonReader jsonReader;
    [SerializeField] GameObject questionPanel;
    
    [Header("Fuel")]
    // [SerializeField] GameObject fuel;
    [SerializeField] Vector2 spawnWaitTimeFuel;

    [Header("Coin")]
    [SerializeField] GameObject coin;
    [SerializeField] Vector2 spawnWaitTimeCoin;


    [Header("Enemy")]
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnWaitTimeEnemy;
    CapsuleCollider2D enemyCollider;

    [Header("Timer")]
    GameObject timer;
    [SerializeField] Vector2 spawnWaitTimeTimer;
    
    [Header("Invisible")]
    GameObject invisiblity;
    [SerializeField] Vector2 spawnWaitTimeInvisiblity;

    [Header("Magnet")]
    GameObject magnet;
    [SerializeField] Vector2 spawnWaitTimeMagnet;
GameObject booster;
        [SerializeField] Vector2 spawnWaitTimeBooster;
     string correctAnswer;
    public string returnCorrectAnswer(){
        return correctAnswer;
    }


    GameObject selectedPlane;
    public PlaneSo[] planeSos;
    public PowerUpSO[] powerUpSOs;
    [SerializeField] TMP_Text questionTxt;
    [SerializeField] TMP_Text[] ansTxt;

    float spawnNextTimeFuel;
    float spawnNextTimeCoin;
    float spawnNextTimeEnemy;
    float spawnNextTimeTimer;
    float spawnNextTimeInvisiblity;
    float spawnNextTimeMagnet;
    float spawnNextTimeBooster;

    Vector2 screenHalfSizeWorldUnits;

    int questionIndex = 0;

    void Awake(){
        // InitializePlayer(); 
    }

    void Start()
    {
        spawnNextTimeFuel = spawnWaitTimeFuel.y;
        
        spawnNextTimeCoin = spawnWaitTimeCoin.y;


        spawnNextTimeEnemy = spawnWaitTimeEnemy.y;
        enemyCollider = enemy.GetComponent<CapsuleCollider2D>();

        spawnNextTimeTimer = spawnWaitTimeTimer.y;
        spawnNextTimeInvisiblity = spawnWaitTimeInvisiblity.y;
        spawnNextTimeMagnet = spawnWaitTimeMagnet.y;
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        magnet = powerUpSOs[0].powerUP;
        invisiblity = powerUpSOs[1].powerUP;
        timer = powerUpSOs[2].powerUP;
        booster = powerUpSOs[3].powerUP;

        StartCoroutine(Question());
        StartCoroutine(Coin());
        
        if(powerUpSOs[0].isPurchased){
            StartCoroutine(Magnet());

        }
        if(powerUpSOs[1].isPurchased){
            StartCoroutine(Invisiblity());
        }

        if(powerUpSOs[2].isPurchased){
            StartCoroutine(Timer());
        }

        if(powerUpSOs[3].isPurchased){
            StartCoroutine(Rocket());
        }
        StartCoroutine(Enemy());
        FindObjectOfType<lowerCollider>().OnPlayerDeath += StopCoroutineQ;
    }
    // Update is called once per frame

    void StopCoroutineQ(){

        StopAllCoroutines();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnNextTimeTimer);
        spawnNextTimeTimer = Mathf.Lerp(spawnWaitTimeTimer.y, spawnWaitTimeTimer.x, difficulty.getDifficultyPercent());
        Instanciate(timer);
        StartCoroutine(Timer());
    }
    private IEnumerator Invisiblity()
    {
        yield return new WaitForSeconds(spawnNextTimeInvisiblity);
        spawnNextTimeInvisiblity = Time.time + Mathf.Lerp(spawnWaitTimeInvisiblity.y, spawnWaitTimeInvisiblity.x, difficulty.getDifficultyPercent());
        Instanciate(invisiblity);
        StartCoroutine(Invisiblity());
    }

    private IEnumerator Magnet()
    {
        yield return new WaitForSeconds(spawnNextTimeMagnet);
        spawnNextTimeMagnet = Time.time + Mathf.Lerp(spawnWaitTimeMagnet.y, spawnWaitTimeMagnet.x, difficulty.getDifficultyPercent());
        Instanciate(magnet);
        StartCoroutine(Magnet());
    }
        private IEnumerator Rocket()
    {
        yield return new WaitForSeconds(spawnNextTimeBooster);
        spawnNextTimeBooster = Time.time + Mathf.Lerp(spawnWaitTimeBooster.y, spawnWaitTimeBooster.x, difficulty.getDifficultyPercent());
        Instanciate(booster);
        StartCoroutine(Rocket());
    }
    
    private void Instanciate(GameObject name)
    {
        Vector2 spawnPoints = new Vector2(screenHalfSizeWorldUnits.x + 1f, Random.Range(-screenHalfSizeWorldUnits.y + .5f, screenHalfSizeWorldUnits.y - .5f));
        Instantiate(name, spawnPoints, Quaternion.identity);
    }

    private IEnumerator Coin()
    {
        yield return new WaitForSeconds(spawnNextTimeCoin);
        spawnNextTimeCoin = Mathf.Lerp(spawnWaitTimeCoin.y, spawnWaitTimeCoin.x, difficulty.getDifficultyPercent());
        Instanciate(coin);
        StartCoroutine(Coin());
    }

    private IEnumerator Enemy()
    {
        yield return new WaitForSeconds(spawnNextTimeEnemy);
        spawnNextTimeEnemy = Mathf.Lerp(spawnWaitTimeEnemy.y, spawnWaitTimeEnemy.x, difficulty.getDifficultyPercent());
        Instanciate(enemy);
        if(FindObjectOfType<PlayerMovement>() != null){
            FindObjectOfType<PlayerMovement>().OnInvisible += DisableCOllider;
            FindObjectOfType<PlayerMovement>().OffInvisible += EnableCollider;
        }
        StartCoroutine(Enemy());
    }

    IEnumerator Question()
    {
        yield return new WaitForSecondsRealtime(spawnNextTimeFuel);
        print(questionIndex);
        correctAnswer = jsonReader.myQuestionList.questions[questionIndex].Correct;
        print(correctAnswer);
        List<string> answerList = new List<string>();

        answerList.Clear();
        answerList.Add(jsonReader.myQuestionList.questions[questionIndex].Correct);
        answerList.Add(jsonReader.myQuestionList.questions[questionIndex].Incorrect1);
        answerList.Add(jsonReader.myQuestionList.questions[questionIndex].Incorrect2);
        answerList.Add(jsonReader.myQuestionList.questions[questionIndex].Incorrect3);
       
        spawnNextTimeFuel = Mathf.Lerp(spawnWaitTimeFuel.y, spawnWaitTimeFuel.x, difficulty.getDifficultyPercent());
        print(spawnNextTimeFuel);
        // settingButtons.ShowQuestionUI();
        questionPanel.SetActive(true);
        OnQuestionEnable?.Invoke();
        Time.timeScale = 0f;
        questionTxt.text = jsonReader.myQuestionList.questions[questionIndex].Question;
        for(int i = 0;i<4;i++){
            int RandomValue = Random.Range(0,answerList.Count-1);
           ansTxt[i].text = answerList[RandomValue];
            answerList.RemoveAt(RandomValue);
        }
        questionIndex++;
            // Instanciate(fuel);
    }

    public void AddFuel(){
        questionPanel.GetComponent<UITween>().CloseSetting();
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(0f,2f));
        StartCoroutine(Question());

    }

    private void DisableCOllider()
    {
        enemyCollider.enabled = false;
    }

    private void EnableCollider()
    {
        enemyCollider.enabled = true;
    }

    void InitializePlayer(){
        for(int i = 0;i<planeSos.Length;i++){
            if(!planeSos[i].isSelected){
                continue;
            }
            else{
                selectedPlane = planeSos[i].planeObject;
            }
            Vector2 spawnPoints = new Vector2(-screenHalfSizeWorldUnits.x + 10f,screenHalfSizeWorldUnits.y - 10f);
            Instantiate(selectedPlane, spawnPoints, Quaternion.identity);
        }
    }
}
