using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private Button _firstReptyButton;
        [SerializeField] private Button _secondReptyButton;
        [SerializeField] private TMP_Text _firstButtonText;
        [SerializeField] private TMP_Text _secondButtonText;
        
        [SerializeField] private TMP_Text _dialogueText;
        
        private Action _firstReplyAction;
        private Action _secondReplyAction;
        
        private Coroutine _dialogueCoroutine;
        private Action _callback;
        
        private string _currentText;
        private bool _dialogueEnd;
        private bool _dialogueStart;
        private bool _canSkip;

        private void Awake()
        {
            _firstReptyButton.onClick.AddListener(FirstAnswer);
            _secondReptyButton.onClick.AddListener(SecondAnswer);
        }

        public void SetDialogueText(Action callback, string text)
        {
            _canSkip = true;
            _callback = null;
            _dialogueEnd = false;
            _dialogueStart = true;
            
            _currentText = text;
            _callback = callback;
            gameObject.SetActive(true);
            _dialoguePanel.SetActive(true);

            HideReplyButtons();
            
            _dialogueCoroutine = StartCoroutine(WriteDialogue(text));
        }

        public void SetAnswers(Action first, Action second, string firstText, string secondText)
        {
            _firstReplyAction = first;
            _secondReplyAction = second;

            _firstButtonText.text = firstText;
            _secondButtonText.text = secondText;
            
            _firstReptyButton.gameObject.SetActive(true);
            _dialoguePanel.SetActive(false);
            if (secondText != "")
            {
                _secondReptyButton.gameObject.SetActive(true);
            }
        }
        
        public bool DisableDialogue()
        {
            if(!_dialogueEnd || !_dialogueStart)
                return false;
            
            _dialogueStart = false;
            _dialogueText.text = "";
            _canSkip = false;
            
            _callback?.Invoke();
            return true;
        }
        
        public void SkipAnimationText()
        {
            if (!_canSkip)
            {
                return;
            }
            
            if (_dialogueEnd || !_dialogueStart)
            {
                return;
            }
            
            Debug.Log("Skip dialogue");
            StopCoroutine(_dialogueCoroutine);
            
            _dialogueText.text = _currentText;
            _dialogueEnd = true;
            _canSkip = false;
        }

        private void FirstAnswer()
        {
            _firstReplyAction?.Invoke();
            HideReplyButtons();
        }
        
        private void SecondAnswer()
        {
            _secondReplyAction?.Invoke();
            HideReplyButtons();
        }

        private void HideReplyButtons()
        {
            _firstReplyAction = null;
            _secondReplyAction = null;
            
            _firstReptyButton.gameObject.SetActive(false);            
            _secondReptyButton.gameObject.SetActive(false);            
        }
        
        private IEnumerator WriteDialogue(string text)
        {
            string currentText = "";
            
            _dialogueText.text = currentText;
            
            char[] letters = text.ToCharArray();
            
            foreach (var letter in letters)
            {
                yield return new WaitForSeconds(0.035f);
                currentText += letter;
                _dialogueText.text = currentText;
            }

            _dialogueEnd = true;
        }
    } 
}

