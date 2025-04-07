using System;
using Game.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.World
{
    public class PlayerBattleController : MonoBehaviour, IService
    {
        private BodyPart _choosedBodyPart;
        private BattleUIController _battleUIController;
        private Player _player;

        private bool _enabled;
        
        public bool CanDestroyed => true;

        public BodyPart ChoosedBodyPart => _choosedBodyPart;
        
        public void Initialize()
        {
            _battleUIController = G.Get<BattleUIController>();
            _player = G.Get<BattleController>().Player;
            
            _battleUIController.BattleUI.Attack.onClick.AddListener(_player.Attack);
            _battleUIController.BattleUI.TryLeave.onClick.AddListener(_player.TryLeave);
            _enabled = true;
        }

        private void Update()
        {
            FindEnemyPoints();
        }
        
        private void FindEnemyPoints()
        {
            if (!_enabled)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 rayPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero);

                if (hit.collider == null)
                {
                    return;
                }
            
                if (hit.collider.TryGetComponent(out BodyPart bodyPart))
                {
                    if (_choosedBodyPart != null)
                    {
                        DisableHighlight();
                    }
                    
                    _choosedBodyPart = bodyPart;
                    
                    _choosedBodyPart.StartHighlight();

                    EnableAttackButton();
                }
            }
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void Enable()
        {
            _enabled = true;
        }
        
        private void EnableAttackButton()
        {
            _battleUIController.BattleUI.Attack.interactable = true;
        }

        private void DisableHighlight()
        {
            _choosedBodyPart.StopHighlight();
        }

        private void OnDestroy()
        {
            _battleUIController.BattleUI.Attack.onClick.RemoveListener(_player.Attack);
            _battleUIController.BattleUI.TryLeave.onClick.RemoveListener(_player.TryLeave);

        }
    }
}
