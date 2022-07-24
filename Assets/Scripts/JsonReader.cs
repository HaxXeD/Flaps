using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;

    [System.Serializable]
    public class Questions{
        public string Question;
        public string Correct;
        public string Incorrect1;
        public string Incorrect2;
        public string Incorrect3;

    }

    [System.Serializable]
    public class QuestionList{
        public Questions[] questions;
    }

    public QuestionList myQuestionList = new QuestionList();
    void Start()
    {   
    //    string questions =  File.ReadAllText("D:/UnityGameDev/flaps/Assets/Scripts/jsonFile.json");
    //    print(questions);
       myQuestionList = JsonUtility.FromJson<QuestionList>(jsonFile.text);
        // print(myQuestionList.questions[0].Correct);
    }
}
