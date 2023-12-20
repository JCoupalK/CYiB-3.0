using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using LeastSquares;

public class HighScore : MonoBehaviour
{
    public Text score;
    public Text highScore;
    private int currentScore;
    private int lastAchievementScore = 0;

    void Start()
    {
        int.TryParse(score.text, out currentScore);
        int storedHighScore = ZPlayerPrefs.GetInt("HighScore", 0);
        highScore.text = ZPlayerPrefs.GetInt("HighScore", 0).ToString();

        CheckAndUnlockAchievements(storedHighScore); // Check for previously earned achievements
        UploadScoreToLeaderboard(storedHighScore); // Upload stored high score to the leaderboard
    }

    void Update()
    {
        UpdateScore();
        CheckForNewAchievements();
    }

    void UpdateScore()
    {
        if (int.TryParse(score.text, out currentScore) && currentScore > ZPlayerPrefs.GetInt("HighScore", 0))
        {
            ZPlayerPrefs.SetInt("HighScore", currentScore);
            highScore.text = currentScore.ToString();
        }
    }

    void CheckForNewAchievements()
    {
        if (currentScore >= lastAchievementScore + 1000)
        {
            lastAchievementScore = currentScore - (currentScore % 1000);
            CheckAchievements(lastAchievementScore);
        }
    }

    void CheckAchievements(int scoreThreshold)
    {
        for (int i = 1; i <= scoreThreshold / 1000; i++)
        {
            SteamLeaderboard steamLeaderboard = FindObjectOfType<SteamLeaderboard>();
            string achievementName = "SKIN_ACH_" + i;
            steamLeaderboard.SetAchievement(achievementName);
            Debug.Log("Achievement set: " + achievementName);
        }
    }


    void CheckAndUnlockAchievements(int highScore)
    {
        for (int i = 1; i <= highScore / 1000; i++)
        {
            SteamLeaderboard steamLeaderboard = FindObjectOfType<SteamLeaderboard>();
            string achievementName = "SKIN_ACH_" + i;
            steamLeaderboard.SetAchievement(achievementName);
            Debug.Log("Achievement set: " + achievementName);
        }
    }

    void UploadScoreToLeaderboard(int highScore)
    {
        SteamLeaderboard steamLeaderboard = FindObjectOfType<SteamLeaderboard>();
        steamLeaderboard.SubmitScore(highScore);
    }



}
