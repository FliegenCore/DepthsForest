using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.World
{
    public class EndController : MonoBehaviour, IService
    {
        private TMP_Text _endText;
        
        public bool CanDestroyed => true;
        
        public void Initialize()
        {
            _endText = FindObjectOfType<TMP_Text>();

            StartCoroutine(WriteText("Thanks for playing", () =>
            {
                StartCoroutine(WriteText("Made on ludum dare 57", () =>
                {
                    StartCoroutine(WriteText("Fliegen....", () =>
                    {
                        StartCoroutine(WriteText("Dima KALABHERS....", () =>
                        {
                            StartCoroutine(WriteText("Disaur....", () =>
                            {
                                // Final callback if needed
                            }));
                        }));
                    }));
                }));
            }));
        }

        private IEnumerator WriteText(string text, Action callback)
        {
            string currentText = "";
            
            _endText.text = currentText;
            
            char[] letters = text.ToCharArray();
            
            foreach (var letter in letters)
            {
                yield return new WaitForSeconds(0.1f);
                currentText += letter;
                _endText.text = currentText;
            }
            
            yield return new WaitForSeconds(2f);
            
            callback?.Invoke();
        }
    } 
}



