using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FlagData
    {
        [SerializeField] private Sprite countryFlag;
        [SerializeField] private string countryName;
        [SerializeField] private List<char> letters;

        public Sprite CountryFlag => countryFlag;
        public string CountryName => countryName;
        public List<char> Letters => letters;
    }
}