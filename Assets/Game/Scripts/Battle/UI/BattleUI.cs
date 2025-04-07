using UnityEngine;
using UnityEngine.UI;

namespace Game.World
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private GameObject _battleButtons;
        [SerializeField] private Button _attack;
        [SerializeField] private Button _openInventory;
        [SerializeField] private Button _defense;
        [SerializeField] private Button _tryLeave;
        
        public Button OpenInventory => _openInventory;
        public Button TryLeave => _tryLeave;
        public Button Attack => _attack;
        public Button Defense => _defense;
        public GameObject BattleButtons => _battleButtons;
    }
}
