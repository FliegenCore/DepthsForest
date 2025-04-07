using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.World
{
 
    public class Background : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetBackground(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}