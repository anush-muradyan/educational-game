using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FlagData
    {
        [SerializeField] private string countryName;
        [SerializeField] private int itemsCount = 16;
        [SerializeField] private Sprite countryFlag;
        // [SerializeField] private List<char> letters;
    
        public string CountryName => countryName;
        public int ItemsCount => itemsCount;
        public Sprite CountryFlag => countryFlag;
        // public List<char> Letters => letters;
    }
}