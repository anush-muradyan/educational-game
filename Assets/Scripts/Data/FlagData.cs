using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FlagData
    {
        [SerializeField] private string countryName;
        [SerializeField] private int itemsCount = 16;
        [SerializeField] private Sprite countryFlag;
        [SerializeField] private PartsOfTheWorld partsOfTheWorld;
    
        public string CountryName => countryName;
        public int ItemsCount => itemsCount;
        public Sprite CountryFlag => countryFlag;
        public PartsOfTheWorld PartsOfTheWorld => partsOfTheWorld;
    }

    public enum PartsOfTheWorld
    {
        Europe,
        Asia,
        Africa,
        NorthAmerica,
        SouthAmerica,
        Australia
    }
}