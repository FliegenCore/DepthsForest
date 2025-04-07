using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.World
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _healthText;
            
        private int _maxHealth;    
        
        public void Initialize(int health)
        {
            _maxHealth = health;
            _slider.maxValue = health;
            _healthText.text = $"{health} / {health}";

            RefreshInfo(health);
        }

        public void RefreshInfo(int currentHealth)
        {
            _healthText.text = $"{currentHealth} / {_maxHealth}";
            _slider.value = currentHealth;
        }
    }
}