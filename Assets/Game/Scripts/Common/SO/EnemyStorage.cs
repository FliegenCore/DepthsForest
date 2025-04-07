using UnityEngine;

namespace Game.World
{
    [CreateAssetMenu(fileName = "enemyStorage", menuName = "game/enemyStorage")]
    public class EnemyStorage : ScriptableObject
    {
        public NPC Enemy;
    }

    
}

