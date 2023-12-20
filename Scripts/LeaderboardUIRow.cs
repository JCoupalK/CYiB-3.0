using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeastSquares
{
    /// <summary>
    /// Class that represents a UI row for a leaderboard entry
    /// </summary>
    public class LeaderboardUIRow : MonoBehaviour
    {
        public Image Avatar;
        public TMP_Text Name;
        public TMP_Text Rank;
        public TMP_Text Score;
    }
}