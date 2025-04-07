using System;
using Assets;
using UnityEngine;

namespace Game.Common
{
    public class DialogueController : MonoBehaviour, IService
    {
        private bool _initialized ;
        private DialogueView _dialogueView;
        
        public bool CanDestroyed => false;
        
        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            

            _initialized = true;
            
            var dialogueWindowAsset = AssetLoader.LoadSync<DialogueView>("Common/DialogueWindow");
            
            _dialogueView = Instantiate(dialogueWindowAsset, transform);
            
            _dialogueView.gameObject.SetActive(false);
            
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(!_dialogueView.DisableDialogue())
                    _dialogueView.SkipAnimationText();
            }
        }

        public void SetAnswers(Action first,string firstText, string secondText = "", Action second = null)
        {
            _dialogueView.SetAnswers(first, second, firstText, secondText);
        }
        
        public void SendMessage(Action callback, string text)
        {
            _dialogueView.SetDialogueText(callback, text);
        }

        public void DisableDialogue()
        {
            _dialogueView.gameObject.SetActive(false);
        }
    }
}
