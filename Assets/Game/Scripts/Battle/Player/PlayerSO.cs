using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.World
{
    [CreateAssetMenu(fileName = "Battle Player", menuName = "game/Battle Player")]
    public class PlayerSO : ScriptableObject
    {
        public int Armor;
        public int Level;
        public int MissChance;
        public int MaxHealth;
        public int CurrentHealth;
        public int Damage;
        
#if UNITY_EDITOR
        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                Level = 0;
                CurrentHealth = 100;
                MaxHealth = 100;
                Damage = 2;
                Armor = 0;
                MissChance = 1;
            }
        }
#endif

        public void ResetAllData()
        {
            Level = 0;
            CurrentHealth = 100;
            MaxHealth = 100;
            Damage = 2;
            Armor = 0;
            MissChance = 1;
        }
    }
}
