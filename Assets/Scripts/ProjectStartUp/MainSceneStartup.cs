using System;
using UnityEngine;
using Zenject;

namespace ProjectStartUp
{
    public class MainSceneStartup: IInitializable, IDisposable 
    {
        public void Initialize()
        {
            Debug.LogError("Initialize");
        }

        public void Dispose()
        {
            Debug.LogError("Dispose");
        }
    }
}