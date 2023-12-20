using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeastSquares;
using Steamworks;
using Steamworks.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeastSquares
{
    /// <summary>
    /// Script to fill the leaderboard UI from the Steam leaderboard
    /// </summary>
    public class LeaderboardUI : MonoBehaviour
    {
        public int EntriesToShowAtOnce = 100;
        public GameObject EntryPrefab;
        public TMP_InputField Input;
        public SteamLeaderboard Leaderboard;
        public LeaderboardType Type = LeaderboardType.Global;
        private List<GameObject> _rows = new();
        private int _offset;

        // Player score stuff:
        public TMP_Text playerScoreText;
        public TMP_Text playerNameText;
        public TMP_Text playerRankText;
        public UnityEngine.UI.Image playerAvatarImage;

        // Global/Friends toggle stuff:
        public Toggle globalToggle;
        public Toggle friendsToggle;


        void Start()
        {
            // Optionally set initial state
            globalToggle.isOn = true;
            friendsToggle.isOn = false;

            globalToggle.onValueChanged.AddListener(OnGlobalToggle);
            friendsToggle.onValueChanged.AddListener(OnFriendsToggle);

            RefreshScores();
        }

        private void OnGlobalToggle(bool isOn)
        {
            if (isOn)
            {
                Type = LeaderboardType.Global;
                RefreshScores();
            }
        }

        private void OnFriendsToggle(bool isOn)
        {
            if (isOn)
            {
                Type = LeaderboardType.Friends;
                RefreshScores();
            }
        }


        /// <summary>
        /// Fill the leaderboardUI with new scores
        /// </summary>
        public async void RefreshScores()
        {
            LeaderboardEntry[] scores;
            switch (Type)
            {
                case LeaderboardType.Global:
                    scores = await Leaderboard.GetScores(EntriesToShowAtOnce - 1, 1 + _offset);
                    break;
                case LeaderboardType.Friends:
                    scores = await Leaderboard.GetScoresFromFriends();
                    break;
                case LeaderboardType.AroundUser:
                    scores = await Leaderboard.GetScoresAroundUser(EntriesToShowAtOnce / 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            RegenerateUI(scores);
        }

        /// <summary>
        /// Renegenerate the leaderboard rows
        /// </summary>
        /// <param name="scores">An array of leaderboard entries</param>
        async void RegenerateUI(LeaderboardEntry[] scores)
        {
            var oldRows = _rows;
            _rows = new List<GameObject>();
            for (var i = 0; i < scores.Length; i++)
            {
                var go = await CreateRow(scores[i]);
                DisplayPlayerScore();

                _rows.Add(go);
            }

            for (var i = 0; i < oldRows.Count; i++)
            {
                Destroy(oldRows[i]);
            }
        }



        /// <summary>
        /// Create a row for the leaderboard entry
        /// </summary>
        /// <param name="entry">The given LeaderboardEntry</param>
        /// <returns>A GameObject representing the row</returns>
        private async Task<GameObject> CreateRow(LeaderboardEntry entry)
        {
            var go = Instantiate(EntryPrefab, transform);
            var row = go.GetComponent<LeaderboardUIRow>();
            row.Score.text = entry.Score.ToString();
            row.Name.text = entry.User.Name;
            row.Rank.text = entry.GlobalRank.ToString();
            var maybeImage = await entry.User.GetSmallAvatarAsync();
            if (maybeImage.HasValue)
            {
                var tex2D = maybeImage.Value.Convert();
                row.Avatar.sprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), Vector2.zero);
            }

            return go;
        }

        // Method to display the player's score
        public async void DisplayPlayerScore()
        {
            var currentPlayerScoreEntry = await Leaderboard.GetBestScore();
            if (currentPlayerScoreEntry != null)
            {
                playerScoreText.text = currentPlayerScoreEntry.Value.Score.ToString();
                playerNameText.text = currentPlayerScoreEntry.Value.User.Name;
                playerRankText.text = currentPlayerScoreEntry.Value.GlobalRank.ToString();

                // Fetch and display the player's avatar
                var maybeAvatar = await currentPlayerScoreEntry.Value.User.GetSmallAvatarAsync();
                if (maybeAvatar.HasValue)
                {
                    var tex2D = maybeAvatar.Value.Convert();
                    playerAvatarImage.sprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f));
                }
            }
            else
            {
                Debug.Log("Failed to fetch player score.");
                playerScoreText.text = "Score not available";
                playerNameText.text = "N/A";
                playerRankText.text = "N/A";
                playerAvatarImage.sprite = null; // Or set to a default image
            }
        }

        /// <summary>
        /// Upload the score in the text field to the leaderboard. Called from the "Save Score" button
        /// </summary>
        public void SaveScore()
        {
            var text = Input.text;
            Leaderboard.SubmitScore(int.Parse(text));
            RefreshScores();
        }

    }

    [Serializable]
    public enum LeaderboardType
    {
        Global,
        Friends,
        AroundUser
    }
}