using System.Collections;
using System.Collections.Generic;
using Assets;
using Game.World;
using UnityEngine;

namespace Game.Common
{
    public class MessageBoxController : MonoBehaviour, IService
    {
        public bool CanDestroyed => false;

        private bool _initialized;
        
        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            DontDestroyOnLoad(gameObject);
        }

        public void ShowMessage(Vector3 position, string message)
        {
            var messageBoxAsset = AssetLoader.LoadSync<MessageBox>("Common/MessageBox");

            MessageBox messageBox = Instantiate(messageBoxAsset, transform);
            messageBox.Show(position, message);
        }
    }
}
