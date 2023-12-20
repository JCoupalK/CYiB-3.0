using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using LeastSquares;

public class AchievementRoofTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision Player)
    {
        SteamLeaderboard steamLeaderboard = FindObjectOfType<SteamLeaderboard>();
        string achievementName = "SECRET_VIEW";
        steamLeaderboard.SetAchievement(achievementName);
        Debug.Log("Achievement set: " + achievementName);
    }
}
