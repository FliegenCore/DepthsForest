using UnityEngine;
using UnityEngine.UI;

namespace Game.World
{
    public class DelivertyView : MonoBehaviour
    {
        [SerializeField] private Button _giftMail;
        
        public Button GiftMailButton => _giftMail;
    }
}
