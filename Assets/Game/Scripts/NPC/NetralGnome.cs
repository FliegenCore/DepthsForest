using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class NetralGnome : NPC, IOnBattleStart, IOnStartMyTurn
    {
        public void BattleStart()
        {
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SetAnswers(() =>
                {
                    G.Get<DialogueController>().SendMessage(() =>
                    {
                        G.Get<DialogueController>().DisableDialogue();
                        G.Get<BattleController>().ChangeTurn();
                        
                    }, "What?! I'll kill you now!");
                    
                }, "Are you all the same?", 
                 "I'm just a postman", () =>
                {
                    G.Get<DialogueController>().SendMessage(() =>
                    {
                        G.Get<DialogueController>().DisableDialogue();
                        StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
                        
                    }, "Okay, go away and don't bother me.");
                });
                
            }, "What are you doing here?!");
        }

        public void StartMyTurn()
        {
            StartCoroutine(WaitAttack());
        }

        private IEnumerator WaitAttack()
        {
            yield return new WaitForSeconds(1f);
            
            Attack();
        }
    }
}
