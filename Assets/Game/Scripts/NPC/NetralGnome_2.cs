using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class NetralGnome_2 : NPC, IOnBattleStart, IOnStartMyTurn
    {
        public void BattleStart()
        {
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().SetAnswers(() =>
                    {
                        G.Get<DialogueController>().SendMessage(() =>
                        {
                            G.Get<DialogueController>().SendMessage(() =>
                            {
                                G.Get<DialogueController>().DisableDialogue();
                                StartCoroutine(G.Get<BattleController>().EndFight(0.5f));
                            }, "Go away, don't touch me!");
                        }, "Oh no... another one...");
                    
                    }, "I killed your mad brother.", 
                    
                    
                    "I'm just a postman.", () =>
                    {
                        G.Get<DialogueController>().SendMessage(() =>
                        {
                            G.Get<DialogueController>().DisableDialogue();
                            G.Get<BattleController>().ChangeTurn();
                            
                        }, "I don't believe you, you're a monster!!");
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