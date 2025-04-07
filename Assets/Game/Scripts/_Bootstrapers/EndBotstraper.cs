using Game.Common;
using Game.World;
using UnityEngine;

namespace Game.Start
{
    public class EndBootstraper : MonoBehaviour
    {
        private void Start()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            G.CreateService<EndController>();
            
            G.InitializeServices();
        }
    }
}