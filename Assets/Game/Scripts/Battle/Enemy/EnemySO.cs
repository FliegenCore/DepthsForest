using UnityEngine;

namespace Game.World
{
    [CreateAssetMenu(fileName = "enemy", menuName = "game/enemy")]
    public class EnemySO : ScriptableObject
    {
        [SerializeField] private NPC _enemy;

        public NPC Enemy => _enemy;
    }
}
