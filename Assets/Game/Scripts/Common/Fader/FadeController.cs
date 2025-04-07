using System;
using Assets;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class FadeController : MonoBehaviour, IService
    {
        private Fade _fade;
        private bool _initialize;
        
        public bool CanDestroyed => false;
        
        public void Initialize()
        {
            if (_initialize)
            {
                return;
            }

            var fadeAsset = AssetLoader.LoadSync<Fade>("Common/Fade");

            _fade = Instantiate(fadeAsset, transform);
            _initialize = true;
            
            DontDestroyOnLoad(gameObject);
            
            FadeOut();
        }

        public void FadeIn(Action callback = null)
        {
            _fade.FadeIn(callback);
        }

        public void FadeOut(Action callback = null)
        {
            _fade.FadeOut(callback);
        }
    }
}
