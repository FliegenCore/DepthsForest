using Game.Common;
using Game.World;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Start
{
    public class MapBootstraper : MonoBehaviour
    {
        private void Start()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            G.CreateService<SceneController>();
            G.CreateService<MapController>();
            G.CreateService<HoverTooltipController>();
            G.CreateService<MapFigureController>();
            G.CreateService<MessageBoxController>();
            G.CreateService<DialogueController>();
            G.CreateService<FadeController>();
            
            G.InitializeServices();
        }
    }
}
