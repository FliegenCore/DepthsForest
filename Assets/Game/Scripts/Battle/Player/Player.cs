using System.Collections;
using System.Collections.Generic;
using Assets;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public static class C
    {
        public static string RED = "ff0000";
        public static string BLUE = "4287f5";
        public static string YELLOW = "ffdd00";
        public static string GREEN = "66fc03";
    }
    
    public class Player : MonoBehaviour, IOnStartMyTurn, IOnLostMyTurn, IBattler, IOnAttack, IOnDamaged, IOnDeath
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private SpriteRenderer _playerSprite;
        
        private List<int> _missChanceList = new List<int>();
        private IBattler _currentOpponent;
        private PlayerSO _playerSO;
        private bool _isDeath;
        private int _currentHealth;
        private int _maxHealth;
        
        public bool IsDeath => _isDeath;

        private void Awake()
        {
            _playerSO = AssetLoader.LoadSync<PlayerSO>("Player/BattlePlayer");
            
            LoadPlayer();
        }

        public void SetOpponent(IBattler battler)
        {
            _currentOpponent = battler;
        }
        
        public void StartMyTurn()
        {
            G.Get<BattleUIController>().EnableBattleWindow();
        }

        public void LostMyTurn()
        {
            G.Get<BattleUIController>().DisableBattleWindow();
        }

        public void TryLeave()
        {
            NPC enemy = G.Get<BattleController>().Enemy;
            G.Get<BattleUIController>().DisableBattleWindow();

            if (enemy.ChanceToLeave <= 0)
            {
                G.Get<DialogueController>().SendMessage(() =>
                {
                    StartCoroutine(PassTurn());
                    G.Get<DialogueController>().DisableDialogue();
                    //G.Get<BattleUIController>().EnableBattleWindow();
                }, "You failed to escape!");
                
                return;
            }

            int roll = Random.Range(1, 11);
            
            if (roll <= enemy.ChanceToLeave)
            {
                // Успешный побег
                G.Get<DialogueController>().SendMessage(() =>
                {
                    StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
                    G.Get<DialogueController>().DisableDialogue();
                }, "You successfully run away!");
            }
            else
            {
                G.Get<DialogueController>().SendMessage(() =>
                {
                    StartCoroutine(PassTurn());
                    G.Get<DialogueController>().DisableDialogue();
                }, "You failed to escape!");
            }
            
        }

        public void Attack()
        {
            G.Get<BattleUIController>().DisableBattleWindow();
            StartCoroutine(WaitAttack());
        }

        private IEnumerator WaitAttack()
        {
            yield return new WaitForSeconds(0.5f);

            //attack animation
            
            yield return new WaitForSeconds(0.5f);
            
            if (_currentOpponent is NPC enemy)
            {
                BodyPart bodyPart = G.Get<PlayerBattleController>().ChoosedBodyPart;
                enemy.TakeDamageOnBodyPart(bodyPart, _playerSO.Damage); 
            }
            
            //add attack effect
            
            StartCoroutine(PassTurn());
        }
        
        private IEnumerator PassTurn()
        {
            yield return new WaitForSeconds(0.5f);
            G.Get<BattleController>().ChangeTurn();
        }

        public void Damaged(int damage)
        {
            if (_missChanceList.Count <= 0)
            {
                FillMissList();
            }
            
            int isMiss = _missChanceList[0];
            _missChanceList.RemoveAt(0);

            if (isMiss == 1)
            {
                G.Get<MessageBoxController>().ShowMessage(transform.position,
                    $"<color=#{C.RED}>MISS!</color>");
                return;
            }
            
            int dmg = damage * _playerSO.Armor / 100;
            
            damage -= dmg;
            
            
            G.Get<MessageBoxController>().ShowMessage(transform.position,
                $"<color=#{C.RED}>{damage}</color>");
            
            _currentHealth -= damage;
            _playerSO.CurrentHealth = _currentHealth;
            
            _healthView.RefreshInfo(_currentHealth);
            
            G.Get<MessageBoxController>().ShowMessage(transform.position, $"<color=#{C.RED}>{damage}</color>");

            if (_currentHealth <= 0)
            {
                Death();
            }
        }

        public void LevelUp(int armor, int maxHealth, int damage, int missChance)
        {
            _playerSO.Level++;
            _playerSO.Damage = damage;
            _playerSO.MaxHealth = maxHealth;
            _playerSO.Armor = armor;
            _playerSO.MissChance = missChance;
            
            LoadPlayer();
        }
        
        public void AddHealth(int amount)
        {
            _currentHealth += amount;
            
            if (_currentHealth >= _playerSO.MaxHealth)
            {
                _currentHealth = _playerSO.MaxHealth;
            }
            
            _playerSO.CurrentHealth = _currentHealth;

            _healthView.RefreshInfo(_currentHealth);
        }

        public void Death()
        {
            _isDeath = true;
            
            //show loose window
            G.Get<PlayerBattleController>().Disable();
            G.Get<LoseGameController>().EndGame();
        }

        public void EnableHealth()
        {
            _healthView.gameObject.SetActive(true);
        }

        public void DisableHealth()
        {
            _healthView.gameObject.SetActive(false);
        }

        private void FillMissList()
        {
            for (int i = 0; i < 10 - _playerSO.MissChance; i++)
            {
                _missChanceList.Add(0);
            }
            
            for (int i = 0; i < _playerSO.MissChance; i++)
            {
                _missChanceList.Add(1);
            }

            BodyPart.Shuffle(_missChanceList);
        }
        
        private void LoadPlayer()
        {
            _currentHealth = _playerSO.CurrentHealth;

            int playerLevel = _playerSO.Level + 1;

            Sprite playerSprite = AssetLoader.LoadSync<Sprite>($"Sprites/Player/player_{playerLevel}");
            _playerSprite.sprite = playerSprite;
            _healthView.Initialize(_playerSO.MaxHealth);
            
            _healthView.RefreshInfo(_currentHealth);
        }
    }
}
