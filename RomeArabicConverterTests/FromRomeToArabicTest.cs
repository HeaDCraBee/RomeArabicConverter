using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomeArabicConverter;

namespace RomeArabicConverterTests
{
    [TestClass]
    public class FromRomeToArabicTest
    {
        [TestMethod]
        public void TestConvert()
        {
            string systemType = "2";

            Converter.Convert(systemType, "CXII");
            Assert.AreEqual("112", Converter.Result);

            Converter.Convert(systemType, "XCIX");
            Assert.AreEqual("99", Converter.Result);

            Converter.Convert(systemType, "MMMMCMXLIX");
            Assert.AreEqual("4949", Converter.Result);
         
            Assert.ThrowsException<ArgumentException>(() => Converter.Convert(systemType, "Z"));
        }
    }
}
