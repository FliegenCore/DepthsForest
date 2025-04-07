using UnityEngine;
using UnityEngine.UI;

namespace Game.Common
{
    public class LoseGameView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        
        public Button RestartButton => _restartButton;
    }
}
