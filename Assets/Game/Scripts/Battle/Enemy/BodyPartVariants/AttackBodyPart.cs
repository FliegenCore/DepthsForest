using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class AttackBodyPart : BodyPart
    {
        public virtual void Attack(Player player, int damage)
        {
            if (!IsDeath)
            {
                player.Damaged(damage);
            }
            else
            {
                G.Get<MessageBoxController>().ShowMessage(transform.position, "I can't attack!");
            }
            
            G.Get<BattleController>().StartCoroutine(WaitChangeTurn());
        }

        public override void Death()
        {
            base.Death();
        }

        private IEnumerator WaitChangeTurn()
        {
            yield return new WaitForSeconds(0.5f);
            
            G.Get<BattleController>().ChangeTurn();
        }
    }
}
