using System.Collections;
using System.Collections.Generic;
using Game.World;
using UnityEngine;

namespace Game.World
{
    public class LegsBodyPart : BodyPart
    {
        [Header("Legs")] 
        [SerializeField] private int _removeMissCount;
        
        private BodyPart[] _bodyParts;
        
        public override void Death()
        {
            Debug.Log("---Legs Death---");
            base.Death();

            Transform parent = transform.parent;
            
            _bodyParts = parent.GetComponentsInChildren<BodyPart>();
            
            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.RemoveMissCount(_removeMissCount);
            }
        }
    }   
}

