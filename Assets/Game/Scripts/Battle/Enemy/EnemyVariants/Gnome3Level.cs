using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class Gnome3Level : NPC, IOnBattleStart, IOnStartMyTurn
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
                        
                    }, "Ok, i'll just kill you!");
                    
                }, "I deliver parcels");
                
            }, "Heey, what are you doing here?!");
        }

        public void StartMyTurn()
        {
            StartCoroutine(WaitAttack());
        }

        private IEnumerator WaitAttack()
        {
            if (WasDamaged)
            {
                G.Get<MessageBoxController>().ShowMessage(transform.position, "Ouch!!!");
            
                yield return new WaitForSeconds(0.7f);
            
                G.Get<MessageBoxController>().ShowMessage(transform.position, "I kill you!!!");

                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.7f);
            }
            
            WasDamaged = false;

            Attack();
        }
    } 
}

