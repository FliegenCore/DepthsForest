using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class Monk : NPC, IOnBattleStart
    {
        [Header("Monk")]
        [SerializeField] private string _startText;
        [SerializeField] private int _healthCount;
        
        private Player _player;

        public void BattleStart()
        {
            _player = FindObjectOfType<Player>();

            Health();
        }

        private void Health()
        {
            if (_startText == "")
            {
                _startText = "Oh... my boy, how did you end up here?";
            }
            
            _player.EnableHealth();

            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SendMessage(() =>
                {
                    _player.AddHealth(_healthCount);
                    
                    G.Get<MessageBoxController>().ShowMessage(_player.transform.position, 
                        $"<color=#{C.GREEN}>{_healthCount}</color>");

                    StartCoroutine(BackOnMap());

                }, "Let me cure you.");
            }, _startText);
        }

        private IEnumerator BackOnMap()
        {
            yield return new WaitForSeconds(0.5f);
            
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().DisableDialogue();
                StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
            }, "Good luck, my boy!");
        }
    }
}
