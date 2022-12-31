using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;

    void Start() {
        _sessionStartTime = DateTime.Now;
        Debug.Log(
        "Game session start @: " + DateTime.Now);
    }
    void OnApplicationQuit() {
        _sessionEndTime = DateTime.Now;
        TimeSpan timeDifference =
        _sessionEndTime.Subtract(_sessionStartTime);
        Debug.Log(
        "Game session ended @: " + DateTime.Now);
        Debug.Log(
        "Game session lasted: " + timeDifference);
    }
}
