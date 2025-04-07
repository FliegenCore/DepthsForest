using Game.Common;
using UnityEngine;

namespace Game.World
{
    public class BattleBootstraper : MonoBehaviour
    {
        private void Start()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            G.CreateService<BattleUIController>();
            G.CreateService<BattleController>();
            G.CreateService<PlayerBattleController>();
            G.CreateService<LoseGameController>();

            G.InitializeServices();
        }
    } 
}

