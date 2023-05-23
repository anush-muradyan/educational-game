using System.Collections.Generic;

namespace Tools
{
    public static class Constants
    {
        public static readonly List<char> RandomLetters = new List<char>()
        {
            'Ա', 'Բ', 'Գ', 'Դ', 'Ե', 'Զ', 'Է',
            'Ը', 'Թ', 'Ժ', 'Ի', 'Լ', 'Խ', 'Ծ',
            'Կ', 'Հ', 'Ձ', 'Ղ', 'Ճ', 'Մ', 'Յ',
            'Ն', 'Շ', 'Ո', 'Չ', 'Պ', 'Ջ', 'Ռ',
            'Ս', 'Վ', 'Տ', 'Ր', 'Ց', 'Փ', 'Ք',
            'Օ', 'Ֆ'
        };

        public const string MUSIC_KEY = "music";
        public const string SOUND_KEY = "sound";
        public const string VOLUME_KEY = "_volume";
    }
}