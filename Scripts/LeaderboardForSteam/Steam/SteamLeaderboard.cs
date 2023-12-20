using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace LeastSquares
{
    /// <summary>
    /// Class that represents a Steam leaderboard.
    /// </summary>
    public class SteamLeaderboard : MonoBehaviour
    {
        public string Name;
        public SteamLeaderboardSort SortType = SteamLeaderboardSort.Descending;
        public SteamLeaderboardDisplay DisplayType = SteamLeaderboardDisplay.Numeric;
        private Leaderboard? _leaderboard;

        /// <summary>
        /// Creates or finds the leaderboard if it doesnt exist. Does not do anything if Steam has not been loaded first.
        /// </summary>
        private async Task CreateOrFindLeaderboard()
        {
            if (_leaderboard != null || !SteamClient.IsValid) return;
            _leaderboard = await SteamUserStats.FindOrCreateLeaderboardAsync(Name, (LeaderboardSort)SortType, (LeaderboardDisplay)DisplayType);
        }

        /// <summary>
        /// Submits a new score to the steam leaderboard. Only updates it if its better than the previous one
        /// </summary>
        /// <param name="newScore">The new score for the user</param>
        public async void SubmitScore(int newScore)
        {
            await CreateOrFindLeaderboard();
            if (_leaderboard != null)
            {
                await _leaderboard.Value.SubmitScoreAsync(newScore);
                Debug.Log($"Sent score {newScore}");
            }
        }

        /// <summary>
        /// Replaces the user's score, even if its lower, with a new one
        /// </summary>
        /// <param name="newScore">The score to replace with</param>
        public async void ReplaceScore(int newScore)
        {
            await CreateOrFindLeaderboard();
            if (_leaderboard != null)
            {
                await _leaderboard.Value.SubmitScoreAsync(newScore);
                Debug.Log($"Sent score {newScore}");
            }
        }

        /// <summary>
        /// Get scores from all users
        /// </summary>
        /// <returns>A list of leaderboard entries from all players</returns>
        public async Task<LeaderboardEntry[]> GetScores(int entriesToRetrieve, int offset = 1)
        {
            await CreateOrFindLeaderboard();
            if (!_leaderboard.HasValue) return await Task.Run(() => new LeaderboardEntry[] { });
            return await _leaderboard.Value.GetScoresAsync(entriesToRetrieve, offset);
        }

        /// <summary>
        /// Get scores around the current steam user
        /// </summary>
        /// <returns>A list of leaderboard entries around the current steam user</returns>
        public async Task<LeaderboardEntry[]> GetScoresAroundUser(int entriesAround)
        {
            await CreateOrFindLeaderboard();
            if (!_leaderboard.HasValue) return await Task.Run(() => new LeaderboardEntry[] { });
            return await _leaderboard.Value.GetScoresAroundUserAsync(-entriesAround, entriesAround);
        }

        /// <summary>
        /// Get scores from friends
        /// </summary>
        /// <returns>A list of leaderboard entries from you and your steam friends</returns>
        public async Task<LeaderboardEntry[]> GetScoresFromFriends()
        {
            await CreateOrFindLeaderboard();
            if (!_leaderboard.HasValue) return await Task.Run(() => new LeaderboardEntry[] { });
            return await _leaderboard.Value.GetScoresFromFriendsAsync();
        }

        /// <summary>
        /// Gets the best score for the current user.
        /// </summary>
        /// <returns>The best leaderboard entry for the current user</returns>
        public async Task<LeaderboardEntry?> GetBestScore()
        {
            await CreateOrFindLeaderboard();
            if (_leaderboard.HasValue)
            {
                var leaderboard = _leaderboard.Value;

                // Fetch a range of scores from the player friends
                var entries = await leaderboard.GetScoresFromFriendsAsync();
                foreach (var entry in entries)
                {
                    if (entry.User.Id == SteamClient.SteamId) // access the Steam ID
                    {
                        return entry;
                    }
                }
            }
            return null;
        }

        public void SetAchievement(string achievementName)
        {
            var ach = new Achievement(achievementName);
            ach.Trigger();
        }
    }

    /// <summary>
    /// Enum representing the sorting method of the leaderboard
    /// </summary>
    [Serializable]
    public enum SteamLeaderboardSort
    {
        [EnumMember]
        Ascending = 1,
        [EnumMember]
        Descending = 2,
    }

    /// <summary>
    /// Enum representing the display method of the leaderboard
    /// </summary>
    [Serializable]
    public enum SteamLeaderboardDisplay
    {
        [EnumMember]
        Numeric = 1,
        [EnumMember]
        TimeSeconds = 2,
        [EnumMember]
        TimeMilliseconds = 3,
    }
}
