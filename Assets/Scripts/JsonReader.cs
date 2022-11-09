using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    SetQuestion set;
    public TextAsset[] jsonFile;

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
        set = FindObjectOfType<SetQuestion>();
        int questionIndex = set.ReturnIndex();
        switch(questionIndex){
            case 0:
                myQuestionList = JsonUtility.FromJson<QuestionList>(jsonFile[0].text);
            break;
            case 1:
            myQuestionList = JsonUtility.FromJson<QuestionList>(jsonFile[1].text);
            break;
            case 2:
            myQuestionList = JsonUtility.FromJson<QuestionList>(jsonFile[2].text);
            break;
        }
    //    string questions =  File.ReadAllText("D:/UnityGameDev/flaps/Assets/Scripts/jsonFile.json");
    //    print(questions);

}
}
