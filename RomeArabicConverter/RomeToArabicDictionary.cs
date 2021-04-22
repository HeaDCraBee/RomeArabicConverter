using System.Collections.Generic;


namespace RomeArabicConverter
{
    class RomeToArabicDictionary
    {
        static Dictionary<char, int> rTADictionary = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000},
        };

        public static Dictionary<char, int> getRTADictionary()
        {
            return rTADictionary;
        }
    }

}
