using System.Collections;
using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class AttackPartWintMinusDamage : AttackBodyPart
    {
        [SerializeField] private int _percentMinusDamage;
        
        public override void Attack(Player player, int damage)
        {
            player.Damaged(damage);

            G.Get<BattleController>().StartCoroutine(WaitChangeTurn());
        }
        
        public override void Death()
        {
            base.Death();
            
            NPC npc = transform.parent.parent.GetComponent<NPC>();
            
            npc.ReduceDamage(_percentMinusDamage);
            
            G.Get<MessageBoxController>().ShowMessage(transform.position, $"-{_percentMinusDamage}% damage");
        }

        private IEnumerator WaitChangeTurn()
        {
            yield return new WaitForSeconds(0.5f);

            G.Get<BattleController>().ChangeTurn();

        }
    }
}
