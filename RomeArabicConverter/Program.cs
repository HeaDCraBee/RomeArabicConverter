using System;

namespace RomeArabicConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Which number system do you want to convert the number to?\n" +
                " 1: from Arabic to Rome\n" +
                " 2: from Rome to Arabic");
            string systemType = Console.ReadLine();
            Console.WriteLine("Enter number");
            string number = Console.ReadLine();
            Converter.Convert(systemType, number);
            Console.WriteLine(Converter.Result);
        }
    }
}
