using UnityEngine;

public static class difficulty
{
    static float secondsToDifficulty = 60f;

    public static float getDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToDifficulty);
    }
}
