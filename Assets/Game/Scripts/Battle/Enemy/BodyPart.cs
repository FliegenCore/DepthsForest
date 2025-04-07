using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class BodyPart : MonoBehaviour, IOnDamaged, ITooltipInformator, IOnDeath
    {
        public event Action<int> OnDamaged;
        
        [SerializeField] private int _attackForDeath;
        
        [SerializeField, Range(0,100)] private int _armor;
        [SerializeField] private string _name;
        [SerializeField] private int _missChanse;
        
        private List<int> _missChanseList = new List<int>();
        
        private Material _material;
        private Sequence _highlightSequence;

        private bool _isDeath;
        
        public string Description => GetDescription();
        
        public int AttackForDeath => _attackForDeath;
        public int Armor => _armor;
        
        public bool IsDeath => _isDeath;
        
        private void Awake()
        {
            _material = GetComponent<SpriteRenderer>().material;

            FillMissList();
        }

        public void Damaged(int damage)
        {
            if (_isDeath)
            {
                return;
            }

            if (_missChanseList.Count <= 0)
            {
                FillMissList();
            }
            
            int isMiss = _missChanseList[0];
            _missChanseList.RemoveAt(0);

            if (isMiss == 1)
            {
                G.Get<MessageBoxController>().ShowMessage(transform.position,
                    $"<color=#{C.RED}>MISS!</color>");
                return;
            }
            
            int dmg = damage * _armor / 100;
            
            damage -= dmg;
            
            _attackForDeath -= 1;
            
            G.Get<MessageBoxController>().ShowMessage(transform.position,
                $"<color=#{C.RED}>{damage}</color>");
            
            OnDamaged?.Invoke(damage);

            if (_attackForDeath <= 0)
            {
                Death();
            }
        }

        public virtual void Death()
        {
            _isDeath = true;

            Disable();
        }
        
        public void StartHighlight()
        {
            _highlightSequence = DOTween.Sequence();               

            _highlightSequence.Append(_material.DOFade(0.5f, 0.5f)).SetLoops(-1, LoopType.Yoyo);
        }

        public void StopHighlight()
        {
            _highlightSequence?.Kill();
            _material.DOFade(1f, 0.5f);
        }

        private string GetDescription()
        {
            int missChance = _missChanse * 10;
            
            return $"{_name} \n <color=#{C.BLUE}>armor: </color>{_armor}% <color=#{C.RED}>miss: </color>{missChance}%";
        }

        private void FillMissList()
        {
            for (int i = 0; i < 10 - _missChanse; i++)
            {
                _missChanseList.Add(0);
            }
            
            for (int i = 0; i < _missChanse; i++)
            {
                _missChanseList.Add(1);
            }

            Shuffle(_missChanseList);
        }
        
        public static void Shuffle<T>(List<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void RemoveMissCount(int count)
        {
            _missChanseList.Clear();
            
            _missChanse -= count;
            
            if (_missChanse <= 0)
            {
                _missChanse = 0;
            }
            
            FillMissList();
        }
        
        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
