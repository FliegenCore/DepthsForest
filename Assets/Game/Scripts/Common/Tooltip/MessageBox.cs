using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.World
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void Show(Vector3 position, string message)
        {
            transform.position = position;
            
            _text.text = message;
            
            transform.DOMoveY(transform.position.y + 1f, 0.5f).OnComplete(() => Destroy(gameObject));
            _text.DOFade(0, 0.5f);
        }
    }
}
