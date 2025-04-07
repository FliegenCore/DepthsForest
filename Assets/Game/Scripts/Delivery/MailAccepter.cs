using UnityEngine;

namespace Game.World
{
    public class MailAccepter : MonoBehaviour
    {
        
    }

    public interface IOnAccept
    {
        void OnAccept();
    }

    public interface IOnStartAccept
    {
        void OnStartAccept();
    }
}
