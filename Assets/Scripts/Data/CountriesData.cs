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
        [SerializeField] private Sprite flag;
        [SerializeField] private Sprite coatOfArms;
        [SerializeField,Multiline] private string description;

        public string CountryName => countryName;
        public string CapitalCity => capitalCity;
        public string NationalLanguage => nationalLanguage;
        public string AreaCount => areaCount;
        public string PopulationCount => populationCount;
        public Sprite Flag => flag;
        public Sprite CoatOfArms => coatOfArms;
        public string Description => description;
    }
}