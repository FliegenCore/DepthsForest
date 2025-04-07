using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class Gnome5Level : NPC, IOnBattleStart, IOnStartMyTurn, IOnHandLoose
    {
        public void BattleStart()
        {
            G.Get<DialogueController>().SendMessage(() =>
            {
                G.Get<DialogueController>().DisableDialogue();
                G.Get<BattleController>().ChangeTurn();
                
            }, "Oaa bee boa");
        }
        
        public void HandLoose()
        {
            G.Get<MessageBoxController>().ShowMessage(transform.position, "A M AMS!!");
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
                G.Get<MessageBoxController>().ShowMessage(transform.position, "MUUUAA!!!");
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