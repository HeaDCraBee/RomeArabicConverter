using System.Collections.Generic;


namespace RomeArabicConverter
{
    class RomeToArabicDictionary
    {
        private static Dictionary<char, int> s_romeToArabic = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000},
        };

        public static Dictionary<char, int> getRomeToArabicDictionary()
        {
            return s_romeToArabic;
        }
    }

}
