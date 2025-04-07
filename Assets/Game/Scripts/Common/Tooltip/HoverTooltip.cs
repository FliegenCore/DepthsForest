using System;
using TMPro;
using UnityEngine;

namespace Game.Common
{
    public class HoverTooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tooltipText;
        [SerializeField] private Vector2 _offset;

        private Vector2 _size;
        
        public Vector2 Offset => _offset;
        
        private void Awake()
        {
            _size = GetComponent<RectTransform>().sizeDelta;
        }

        public void Show(string text)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                _tooltipText.text = text;
            }
            
            //transform.position = Input.mousePosition;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
