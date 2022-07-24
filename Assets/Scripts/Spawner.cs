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
    [SerializeField] GameObject timer;
    [SerializeField] Vector2 spawnWaitTimeTimer;
    
    [Header("Invisible")]
    [SerializeField] GameObject invisiblity;
    [SerializeField] Vector2 spawnWaitTimeInvisiblity;

    [Header("Magnet")]
    [SerializeField] GameObject magnet;
    [SerializeField] Vector2 spawnWaitTimeMagnet;
     string correctAnswer;
    public string returnCorrectAnswer(){
        return correctAnswer;
    }

    [SerializeField] TMP_Text questionTxt;
    [SerializeField] TMP_Text[] ansTxt;

    float spawnNextTimeFuel;
    float spawnNextTimeCoin;
    float spawnNextTimeEnemy;
    float spawnNextTimeTimer;
    float spawnNextTimeInvisiblity;
    float spawnNextTimeMagnet;

    Vector2 screenHalfSizeWorldUnits;

    int questionIndex = 0;
    // Start is called before the first frame update
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
        StartCoroutine(Question());
        FindObjectOfType<lowerCollider>().OnPlayerDeath += StopCoroutineQ;
    }

    void StopCoroutineQ(){
        StopCoroutine(Question());
    }
    // Update is called once per frame
    void Update()
    {
        // Fuel();
        Coin();
        Enemy();
        Timer();
        Invisiblity();
        Magnet();
    }

    private void Timer()
    {
        if (Time.time > spawnNextTimeTimer)
        {
            spawnNextTimeTimer = Time.time + Mathf.Lerp(spawnWaitTimeTimer.y, spawnWaitTimeTimer.x, difficulty.getDifficultyPercent());
            Instanciate(timer);
        }
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

        // if(correctAnswer == transform.GetChild(0).GetComponent<TMP_Text>().text){
        //     fillScript.GetComponent<Image>().fillAmount = 1;
        // }
        // correctAnswer == answer?fillScript.GetComponent<Image>().fillAmount = 1;
        // settingButtons.HideQuestionUI();
        questionPanel.GetComponent<UITween>().CloseSetting();
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine(FindObjectOfType<EaseUp>().PowerUpTime(0f,2f));
        StartCoroutine(Question());

    }
    // IEnumerator PowerUpTime(float time, float timertime)
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



    private void Instanciate(GameObject name)
    {
        Vector2 spawnPoints = new Vector2(screenHalfSizeWorldUnits.x + .5f, Random.Range(-screenHalfSizeWorldUnits.y + .5f, screenHalfSizeWorldUnits.y - .5f));
        Instantiate(name, spawnPoints, Quaternion.identity);
    }

    private void Coin()
    {
        if (Time.time > spawnNextTimeCoin)
        {
            spawnNextTimeCoin = Time.time + Mathf.Lerp(spawnWaitTimeCoin.y, spawnWaitTimeCoin.x, difficulty.getDifficultyPercent());
            Instanciate(coin);
        }
    }


    private void Enemy()
    {
        if (Time.time > spawnNextTimeEnemy)
        {
            spawnNextTimeEnemy = Time.time + Mathf.Lerp(spawnWaitTimeEnemy.y, spawnWaitTimeEnemy.x, difficulty.getDifficultyPercent());
            Instanciate(enemy);
            if(FindObjectOfType<PlayerMovement>() != null){
                FindObjectOfType<PlayerMovement>().OnInvisible += DisableCOllider;
                FindObjectOfType<PlayerMovement>().OffInvisible += EnableCollider;
            }
        }
    }

    private void DisableCOllider()
    {
        enemyCollider.enabled = false;
    }

    private void EnableCollider()
    {
        enemyCollider.enabled = true;
    }

    private void Invisiblity()
    {
        if (Time.time > spawnNextTimeInvisiblity)
        {
            spawnNextTimeInvisiblity = Time.time + Mathf.Lerp(spawnWaitTimeInvisiblity.y, spawnWaitTimeInvisiblity.x, difficulty.getDifficultyPercent());
            Instanciate(invisiblity);
        }
    }

        private void Magnet()
    {
        if (Time.time > spawnNextTimeMagnet)
        {
            spawnNextTimeMagnet = Time.time + Mathf.Lerp(spawnWaitTimeMagnet.y, spawnWaitTimeMagnet.x, difficulty.getDifficultyPercent());
            Instanciate(magnet);
        }
    }
    
}
