using UnityEngine;

namespace Game.World
{
    public class BattleUIController : MonoBehaviour, IService
    {
        private BattleUI _battleUI;
        
        public bool CanDestroyed => true;
        public BattleUI BattleUI => _battleUI;
        
        public void Initialize()
        {
            _battleUI = FindObjectOfType<BattleUI>();
            //start dialogue + 
        }

        public void EnableBattleButtons()
        {
            _battleUI.BattleButtons.SetActive(true);
        }

        public void DisableBattleButtons()
        {
            _battleUI.BattleButtons.SetActive(false);
        }

        public void EnableBattleWindow()
        {
            _battleUI.gameObject.SetActive(true);
        }
        
        public void DisableBattleWindow()
        {
            _battleUI.gameObject.SetActive(false);
        }
    }
}
