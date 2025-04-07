using UnityEngine;

namespace Game.World
{
    [CreateAssetMenu(fileName = "mailAccepter", menuName = "game/mailAccepter")]
    public class MailAccepterStorage : ScriptableObject
    {
        public MailAccepter MailAccepter;
        public bool NpcIsRight;
        public Sprite LocationSprite;
    }
}
