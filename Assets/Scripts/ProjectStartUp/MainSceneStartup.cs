using System;
using UI.Flows;
using UnityEngine;
using Zenject;

namespace ProjectStartUp
{
    public class MainSceneStartup: IInitializable, IDisposable
    {
        private FlowNavigator _flowNavigator;

        public MainSceneStartup(FlowNavigator flowNavigator)
        {
            _flowNavigator = flowNavigator;
        }

        public void Initialize()
        {
            _flowNavigator.Start();
            Debug.LogError("Initialize");
        }

        public void Dispose()
        {
            Debug.LogError("Dispose");
        }
    }
}