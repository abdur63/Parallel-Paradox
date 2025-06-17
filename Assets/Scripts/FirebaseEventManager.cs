using Firebase.Analytics;
using UnityEngine;

public class FirebaseEventManager : MonoBehaviour
{
    public static FirebaseEventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LogLevelStart(int levelNumber)
    {
        FirebaseAnalytics.LogEvent("Level_Start", "Level_Number", levelNumber);
        Debug.Log("Start logged" + "   " + levelNumber);
    }

    public void LogLevelComplete(int levelNumber)
    {
        FirebaseAnalytics.LogEvent("Level_Complete", "Level_Number", levelNumber);
    }
}
