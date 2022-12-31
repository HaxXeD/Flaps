using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestionSpawner : MonoBehaviour
{
    //serialize TimeScaleController
    [SerializeField] TimeScaleController timeScaleController;

    //serialize JsonReader
    [SerializeField] JsonReader jsonReader;

    //Serialize Question Panel<UI>
    [SerializeField] GameObject questionPanel;

    //serialize wait time to set Question Panel to active
    [SerializeField] Vector2 setActiveWaitTimeQuestion;

    //serialize EaseUp
    [SerializeField] EaseUp easeUp;

    //serialize question text 
    [SerializeField] TMP_Text questionTxt;
    
    //serialize answer text array
    [SerializeField] TMP_Text[] ansTxt;

    //serialize lower collider
    [SerializeField] lowerCollider lowerCollider;

    //float for next wait time calculation
    private float _setActiveNextTimeQuestion;

    //is question panel active boolean 
    private bool _isQuesitonPanelActive = false;

    //string for correct answer
    private string _correctAnswer;

    //returns correct answer
    public string returnCorrectAnswer() => _correctAnswer;

    //current index of the question
    private int _questionIndex;

    //Lists of answers
    List<string> answerList = new List<string>();

    //event for when qusetion panel is active and inactive
    public event System.Action OnQuestionPanelActive,
                                OnQuestionPanelInactive;
    public IEnumerator I_QuestionPanelActiveSpan;

    Coroutine C_QuestionPanelSetActive,
                C_SetQuestionInactiveIfUnanswered;

    void Start()
    {

        //When paused stop the question set active coroutine
        timeScaleController.OnPause.AddListener(StopQuesitonPanelActiveCoroutine);
        //when unpaused restart it
        timeScaleController.OnUnpause.AddListener(StartQuestionPanelActiveCoroutine);
        
        _setActiveNextTimeQuestion = setActiveWaitTimeQuestion.x;

        StartQuestionPanelActiveCoroutine();
        
        lowerCollider.OnPlayerDeath += StopQuesitonPanelActiveCoroutine;
    }

    private void StartQuestionPanelActiveCoroutine() => C_QuestionPanelSetActive = StartCoroutine(SetQuestionPanelActive());

    private void StopQuesitonPanelActiveCoroutine() => StopCoroutine(C_QuestionPanelSetActive);

    IEnumerator SetQuestionPanelActive()
    {
        //set quesiton panel active to true
        _isQuesitonPanelActive = true;

        //randomly select a queston form the parsed json file
        //which resides in a list and store it in the question index integer
        _questionIndex = Random.Range(0,jsonReader.myQuestionList.questions.Count);
        
        //store the correct answer form the list in the correct answer string
        _correctAnswer = jsonReader.myQuestionList.questions[_questionIndex].Correct;
        print(_correctAnswer);

        //clear the answer list
        answerList.Clear();

        //add the correct and remaining 3 incorrect answers to the list
        answerList.Add(jsonReader.myQuestionList.questions[_questionIndex].Correct);
        answerList.Add(jsonReader.myQuestionList.questions[_questionIndex].Incorrect1);
        answerList.Add(jsonReader.myQuestionList.questions[_questionIndex].Incorrect2);
        answerList.Add(jsonReader.myQuestionList.questions[_questionIndex].Incorrect3);

        //wait time to set question panel to active
        yield return new WaitForSecondsRealtime(_setActiveNextTimeQuestion);

        //calculate the next wait time 
        _setActiveNextTimeQuestion = Mathf.Lerp(setActiveWaitTimeQuestion.y, setActiveWaitTimeQuestion.x, difficulty.getDifficultyPercent());

        //invoke event for when quesiton panel is set to active
        OnQuestionPanelActive?.Invoke();
        print("Question Event Invoked");

        //start coroutine to hide the question panel 
        //if the answer is not given in x amount of time
        C_SetQuestionInactiveIfUnanswered = StartCoroutine(SetQuestionInactiveIfUnanswered());

        //set question panel to active
        questionPanel.SetActive(true);

        //display the question in the question text object
        questionTxt.text = jsonReader.myQuestionList.questions[_questionIndex].Question;

        //run a loop to put answers for the answer list
        //randomly in the answer arry
        for(int i = 0;i<4;i++){
            int RandomValue = Random.Range(0,answerList.Count);
            ansTxt[i].text = answerList[RandomValue];
            //remove the inserted value so it doesn't repeat
            answerList.RemoveAt(RandomValue);
        }
    }

    //set the question panel inactive if answer is not given
    IEnumerator SetQuestionInactiveIfUnanswered(){
        //wait for the given time
        yield return new WaitForSecondsRealtime(5f);;
        //check if the question panel is inactive, if true exit else
        if(!_isQuesitonPanelActive)yield return null;
        //Set the question panel to inactive
        SetQuestionInactive();
    }
       
    //set the quesiton panel to inactive
    public void SetQuestionInactive(){
        //if the set question inactive if unanswered is running then stop it
        if(C_SetQuestionInactiveIfUnanswered!=null)StopCoroutine(C_SetQuestionInactiveIfUnanswered);
        //set the question panel active boolean to false
        _isQuesitonPanelActive = false;
        //close the quesiton panel -> set it inactive
        questionPanel.GetComponent<UITween>().CloseSetting();
        //invoke question panel inactive event
        OnQuestionPanelInactive?.Invoke();
        //cache to slowly ease in the timescale to 1
        I_QuestionPanelActiveSpan = easeUp.ScaleTime(Time.timeScale,1f,2f);
        //start the ease in coroutine
        StartCoroutine(I_QuestionPanelActiveSpan);
        //remove the showed question form the question list
        jsonReader.myQuestionList.questions.RemoveAt(_questionIndex);
        //restart the question panel set active coroutine
        C_QuestionPanelSetActive = StartCoroutine(SetQuestionPanelActive());
    }
}

