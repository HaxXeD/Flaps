using UnityEngine;
using System.Collections.Generic;

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
        public List<Questions> questions;
    }

    public QuestionList myQuestionList = new QuestionList();
    void Awake() => myQuestionList = JsonUtility.FromJson<QuestionList>(jsonFile.text);
}
