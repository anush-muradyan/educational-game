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
        [SerializeField] private Sprite flag;
        [SerializeField] private Sprite coatOfArms;
        [SerializeField] private string nationalLanguage;
        [SerializeField,Multiline] private string description;


        public string CountryName => countryName;
        public string CapitalCity => capitalCity;
        public Sprite Flag => flag;
        public Sprite CoatOfArms => coatOfArms;
        public string NationalLanguage => nationalLanguage;
        public string Description => description;
    }
}