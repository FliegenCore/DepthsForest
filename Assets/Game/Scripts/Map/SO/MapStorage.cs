using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.World
{
    [CreateAssetMenu(fileName = "Map", menuName = "game/map")]
    public class MapStorage : ScriptableObject
    {
        public int CurrentPlayerWaypointIndex;
        public float CurrentDepth;
        public float LastDepth;
        public Dictionary<int, WaypointType> Waypoints = new Dictionary<int, WaypointType>();
        
#if UNITY_EDITOR
        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                Waypoints.Clear();
                CurrentPlayerWaypointIndex = -1;
                LastDepth = 0;
                CurrentDepth = 0;
            }
        }
#endif

        public void ResetAllData()
        {
            Waypoints.Clear();
            CurrentPlayerWaypointIndex = -1;
            LastDepth = 0;
            CurrentDepth = 0;
        }
    }
}
