using System.Collections;
using Game.Common;
using UnityEngine;


namespace Game.World
{
    public class GoodGnome : NPC, IOnBattleStart, IOnStartMyTurn
    {
        private Player _player;

        public void BattleStart()
        {
            _player = FindObjectOfType<Player>();

            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SendMessage(() =>
                {
                    G.Get<DialogueController>().SendMessage(() =>
                    {
                        G.Get<DialogueController>().DisableDialogue();
                        G.Get<BattleController>().ChangeTurn();
                        
                    }, "Choose a point on me that you want to hit, then press attack");
                }, "But first I will teach you how to fight");
            }, "What?! Human!! I will kill you!");
        }
        
        public override void Death()
        {
            _isDeath = true;
            DisableHealth();
            _player.DisableHealth();
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SendMessage(() =>
                {
                    G.Get<DialogueController>().SendMessage(EndDialogue, "Good luck!");
                    
                }, "My brother lives up there, he will help you.");
                
            }, "Stop please, don't kill me.");
        }
        
        private void EndDialogue()
        {
            G.Get<DialogueController>().DisableDialogue();
            StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
        }

        public void StartMyTurn()
        {
            StartCoroutine(WaitMessage());
        }

        private IEnumerator WaitMessage()
        {
            G.Get<MessageBoxController>().ShowMessage(transform.position, "You");
            yield return new WaitForSeconds(0.5f);
            G.Get<MessageBoxController>().ShowMessage(transform.position, "Won't");
            yield return new WaitForSeconds(0.5f);
            G.Get<MessageBoxController>().ShowMessage(transform.position, $"<color=#{C.RED}>ESCAPE!</color>");
            G.Get<BattleController>().ChangeTurn();
        }
    }
}
