using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CountryData", menuName = "ScriptableObjects/CountryData")]

    public class CountriesData : ScriptableObject
    {
        [SerializeField] private List<CountryData> countryData;

        public List<CountryData> CountryData => countryData;
    }

    [Serializable]
    public class CountryData
    {
        [SerializeField] private string countryName;
        [SerializeField] private string capitalCity;
        [SerializeField] private string areaCount;
        [SerializeField] private string populationCount;
        [SerializeField] private string nationalLanguage;
        [SerializeField] private string monetaryUnit;
        [SerializeField,Multiline] private string description;
        [SerializeField] private Sprite flag;
        [SerializeField] private Sprite coatOfArms;

        public string CountryName => countryName;
        public string CapitalCity => capitalCity;
        public string AreaCount => areaCount;
        public string PopulationCount => populationCount;
        public string NationalLanguage => nationalLanguage;
        public string MonetaryUnit => monetaryUnit;
        public string Description => description;
        public Sprite Flag => flag;
        public Sprite CoatOfArms => coatOfArms;
    }
}