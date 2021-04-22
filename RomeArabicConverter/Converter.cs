using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomeArabicConverter
{
    public class Converter
    {
        delegate double PowTen();
        static string result;
        public static void Convert(string systemType, string number)
        {
            
            if (systemType == "1")
            {
                Result = ToRome(int.Parse(number));
            }
            else if (systemType == "2")
            {
                Result = ToArabic(number.ToCharArray()).ToString();
            }
        }

        public static string Result { get; private set; }
        
        

        static int ToArabic(char[] number)
        {
            var romeToArabicDictionary = RomeToArabicDictionary.getRTADictionary();
            foreach (var chars in number)
            {
                if (!romeToArabicDictionary.ContainsKey(chars))
                {
                    throw new ArgumentException("Incorrect sentence");
                }
            }
            int res = romeToArabicDictionary[number[number.Length - 1]];
            bool isLover = false;
            int bigger = 0;
            
            /*
             * Начиная с конца строки начинает перевод из римских в арабские(справа на лево)
             * Если раньше встретилось число больше, то нынешнее число вычитается из текущего значения res.
             * Большее число запоминается, чтобы исключить позиции типа "IIX"(несколько отрицаний)
             * Если же нынешнее число больше предыдущего, то оно прибавляется.
             * Так идя по числу CDXCIX(499) сначала посчитается последнее X(res = 10), из него вычтеться I (res = 9), 
             * к полученному результату прибавится С (res = 109), вычтится X (res = 99), прибавиться D (res = 599) и вычтиться C (res = 499)
             */
            for (int i = number.Length - 2; i > -1; i--)
            {
                if (romeToArabicDictionary[number[i]] < romeToArabicDictionary[number[i + 1]] && !isLover)
                {
                    res -= romeToArabicDictionary[number[i]];
                    isLover = true;
                    bigger = romeToArabicDictionary[number[i + 1]];
                }
                else if (isLover && (romeToArabicDictionary[number[i]] < bigger))
                {
                    throw new ArgumentException("Incorrect sentence");
                }
                else
                {
                    res += romeToArabicDictionary[number[i]];
                    isLover = false;
                }
            }
            return res;
        }

        /*
         * Обрабатывает десятичные разряды, переводя их в римскую систему, начиная с большего разряда
         * Например 296 -> Сначала 200 переводится в СС, далее 90 переводиться в XC, затем 6 переводится в VI
         */
        public static string ToRome(int number)
        {
            Dictionary<int, char> arabicToRomeDictionary = new Dictionary<int, char>();
            int maxValueInRome = 0;
            foreach (var pair in RomeToArabicDictionary.getRTADictionary())
            {
                arabicToRomeDictionary.Add(pair.Value, pair.Key);
                maxValueInRome = pair.Value;
            }
            
            int length = number.ToString().Length;
            StringBuilder romeStringBuilder = new StringBuilder();
            if (length >= maxValueInRome.ToString().Length)
            {
                for (int i = 0; i < number / maxValueInRome; i++)
                {
                    romeStringBuilder.Append(arabicToRomeDictionary[maxValueInRome]);
                }
                number %= maxValueInRome;
                length = number.ToString().Length;
            }

            while (number != 0)
            {
                char smallerChar = arabicToRomeDictionary[(int)Math.Pow(10, length - 1)];
                char midleChar = arabicToRomeDictionary[5 * (int)Math.Pow(10, length - 1)];
                char biggerChar = arabicToRomeDictionary[(int)Math.Pow(10, length)];
                romeStringBuilder.Append(AppendRomeStrings(number, powTen: () => Math.Pow(10, length - 1), smallerChar, midleChar, biggerChar));
                number %= (int)Math.Pow(10, length - 1);
                length = number.ToString().Length;
            }
            return romeStringBuilder.ToString();
        }

        /* В данный метод передается текущее число, степень 10 и необходимые римские цифры
         * Далее, в зависимости от значащей части числа вычесляется необходимая комбинация римских чисел.
         * При этом не важна разрядность самого числа, римские числа выставляются в зависимости от значимого разряда =>
         * Что в 400 что в 40 будет рассмотрена сама 4ка
         * Сами же числа будут выставленны исходя из переданных
         */
        static string AppendRomeStrings(int number, PowTen powTen, char smallerChar, char midleChar, char biggerChar)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (number / (int)powTen() < 4)
            {

                for (int i = 0; i < number / (int)powTen(); i++)
                {
                    stringBuilder.Append(smallerChar);
                }
            }
            else if (number / (int)powTen() == 4)
            {
                stringBuilder.Append(smallerChar);
                stringBuilder.Append(midleChar);
            }
            else if (number / (int)powTen() < 9)
            {
                stringBuilder.Append(midleChar);
                for (int i = number / (int)powTen() - 5; i > 0; i--)
                {
                    stringBuilder.Append(smallerChar);
                }
            }
            else
            {
                stringBuilder.Append(smallerChar);
                stringBuilder.Append(biggerChar);
            }
            return stringBuilder.ToString();
        }
    }
}
