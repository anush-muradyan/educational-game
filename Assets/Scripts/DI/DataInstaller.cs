using Data;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "DataInstaller", menuName = "Installers/DataInstaller")]
    public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
    {
        [SerializeField] private FlagsQuizData flagsQuizData;
        [SerializeField] private CountriesData countriesData;
        public override void InstallBindings()
        {
            Container.BindInstance(flagsQuizData);
            Container.BindInstance(countriesData);
        }
    }
}