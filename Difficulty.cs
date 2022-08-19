
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    static float scoreToMaxDifficulty = 14000f;
    public static float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Gameover.score / scoreToMaxDifficulty); 
    }
}
