using Game.Common;
using Game.World;
using UnityEngine;

namespace Game.Start
{
    public class StartBootstraper : MonoBehaviour
    {
        private void Start()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            G.CreateService<SceneController>();
            G.CreateService<FadeController>();
            
            G.InitializeServices();
        }
    }
}