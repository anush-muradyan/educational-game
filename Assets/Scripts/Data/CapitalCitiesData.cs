using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CapitalCityData", menuName = "ScriptableObjects/CapitalCityData")]
    public class CapitalCitiesData : ScriptableObject
    {
        [SerializeField] private List<CapitalCityData> capitalCityData;

        public List<CapitalCityData> CapitalCityData => capitalCityData;
    }


    [Serializable]
    public class CapitalCityData
    {
        [SerializeField] private string countryName;
        [SerializeField] private string capitalCityName;
        [SerializeField] private List<char> letters;

        public string CountryName => countryName;
        public string CapitalCityName => capitalCityName;
        public List<char> Letters => letters;
    }
}