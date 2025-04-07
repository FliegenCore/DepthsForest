using System.Collections;
using System.Collections.Generic;
using Game.World;
using UnityEngine;

namespace Game.Common
{
    public class DeliveryBootstraper : MonoBehaviour
    {
        private void Start()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            G.CreateService<DeliveryController>();
            
            G.InitializeServices();
        }
    }

}
