using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class Gnome4Level : NPC, IOnBattleStart, IOnStartMyTurn, IOnHandLoose
    {
        public void BattleStart()
        {
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().DisableDialogue();
                G.Get<BattleController>().ChangeTurn();
                
            }, "Attack, assh*le! I will finish you off");
        }
        
        public void HandLoose()
        {
            G.Get<MessageBoxController>().ShowMessage(transform.position, "Kill me please!");
            StartCoroutine(WaitPassTurn());
        }
        
        public void StartMyTurn()
        {
            StartCoroutine(WaitAttack());
        }

        private IEnumerator WaitAttack()
        {
            if (WasDamaged)
            {
                G.Get<MessageBoxController>().ShowMessage(transform.position, "AAAA!!!");
            }
            
            yield return new WaitForSeconds(0.7f);
            
            WasDamaged = false;

            Attack();
        }

        private IEnumerator WaitPassTurn()
        {
            yield return new WaitForSeconds(0.5f);
            G.Get<BattleController>().ChangeTurn();
        }
        
    } 
}