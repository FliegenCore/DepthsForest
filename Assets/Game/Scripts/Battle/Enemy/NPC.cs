using System.Collections;
using DG.Tweening;
using Game.Common;
using TMPro;
using UnityEngine;

namespace Game.World
{
    public class NPC : MonoBehaviour, IBattler, IOnAttack, IOnDamaged, IOnDeath
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private string _name;
        [SerializeField] private int _damage; //todo move in scriptable object
        [SerializeField] private int _maxHealth;//todo move in scriptable object
        [SerializeField] private int _chanceToLeave;
        
        private BodyPart[] _bodyParts;
        private int _currentHealth;
        protected bool _isDeath;
        
        private IBattler _battler;

        public bool IsDeath => _isDeath;
        public bool WasDamaged { get; set; }
        public int ChanceToLeave => _chanceToLeave;

        private void Awake()
        {
            _bodyParts = transform.GetComponentsInChildren<BodyPart>();
            _currentHealth = _maxHealth;
            _nameText.text = _name;
            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.OnDamaged += Damaged;
            }
            
            if(_healthView != null)
                _healthView.Initialize(_maxHealth);
            
            RecalculateBodyParts();
        }

        public void SetOpponent(IBattler battler)
        {
            _battler = battler;
        }
        
        public void Damaged(int damage)
        {
            WasDamaged = true;
            _currentHealth -= damage;
            
            if (_currentHealth <= 0)
            {
                Death();
                _currentHealth = 0;
            }
            
            _healthView.RefreshInfo(_currentHealth);
            RecalculateBodyParts();
        }
        
        public void Attack()
        {
            if (_battler == null)
            {
                return;
            }
            
            foreach (var bodyPart in _bodyParts)
            {
                if (bodyPart is AttackBodyPart attackPoint)
                {
                    attackPoint.Attack(_battler as Player, _damage);
                }
            }
        }

        public void TakeDamageOnBodyPart(BodyPart bodyPart, int damage)
        {
            bodyPart.Damaged(damage);
        }
        
        private void RecalculateBodyParts()
        {
            foreach (var bodyPart in _bodyParts)
            {
                if (bodyPart is HeadPointBodyPart headPoint)
                {
                    if (headPoint.AttackForDeath <= 0)
                    {
                        Death();
                    }
                }
            }
        }

        public void ReduceDamage(int percentCount)
        {
            if (percentCount < 0 || percentCount > 100)
            {
                return;
            }

            float reductionMultiplier = 1f - (percentCount / 100f);
    
            _damage = Mathf.RoundToInt(_damage * reductionMultiplier);
    
            _damage = Mathf.Max(0, _damage);
        }
        
        public virtual void Death()
        {
            StartCoroutine(Hide());
            _isDeath = true;
        }
        
        public void EnableHealth()
        {
            if(_healthView != null)
                _healthView.gameObject.SetActive(true);
        }

        public void DisableHealth()
        {
            if(_healthView != null)
                _healthView.gameObject.SetActive(false);
        }
        
        private IEnumerator Hide()
        {
            yield return new WaitForSeconds(1f);
            
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (var sprite in spriteRenderers)
            {
                sprite.DOFade(0, 1)
                    .OnComplete(() =>
                    {
                        StartCoroutine(G.Get<BattleController>().EndFight(1.5f));
                    });
            }
        }
    }
}
