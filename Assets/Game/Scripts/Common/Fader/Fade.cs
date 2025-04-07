using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;

        public void FadeIn(Action callback)
        {
            gameObject.SetActive(true);
            _fadeImage.DOFade(1, 1f).OnComplete(() =>
            {
                callback?.Invoke();
            });
        }

        public void FadeOut(Action callback)
        {
            _fadeImage.DOFade(0, 1f).OnComplete(() =>
            {
                callback?.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}
