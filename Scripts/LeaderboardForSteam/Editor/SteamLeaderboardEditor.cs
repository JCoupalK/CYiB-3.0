using System;
using UnityEditor;
using UnityEngine;

namespace LeastSquares
{

    [CustomEditor(typeof(SteamLeaderboard))]
    [CanEditMultipleObjects]
    public class SteamLeaderboardEditor : Editor
    {
        private SerializedProperty _sortType;
        private SerializedProperty _displayType;
        
        private void OnEnable()
        {
            _sortType = serializedObject.FindProperty("SortType");
            _displayType = serializedObject.FindProperty("DisplayType");
        }

        public override void OnInspectorGUI()
        {
            var leaderboard = (SteamLeaderboard)target;
            EditorGUILayout.Separator();
            GUILayout.Label("Leaderboard Settings", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            leaderboard.Name = EditorGUILayout.TextField(
                new GUIContent("Leaderboard Name", "The name for the leaderboard. If it does not exist a new one will be created with this name"),
                leaderboard.Name
            );
            
            EditorGUILayout.PropertyField(
                _sortType,
                new GUIContent("Sort Type", "The sort type for the leaderboard entries, this can be either ascending or descending")
            );
            
            EditorGUILayout.PropertyField(
                _displayType,
                new GUIContent("Display Type", "The display type for the leaderboard values, this can be numeric, time in seconds or time in millis")
            );

            serializedObject.ApplyModifiedProperties();
            EditorGUI.indentLevel--;
        }
    }
}