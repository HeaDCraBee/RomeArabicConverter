using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomeArabicConverter;

namespace RomeArabicConverterTests
{
    [TestClass]
    public class FromArabicToRomeTest
    {
        [TestMethod]
        public void TestConvert()
        {
            string systemType = "1";

            Converter.Convert(systemType, "112");
            Assert.AreEqual("CXII", Converter.Result);

            Converter.Convert(systemType, "99");
            Assert.AreEqual("XCIX", Converter.Result);

            Converter.Convert(systemType, "4949");
            Assert.AreEqual("MMMMCMXLIX", Converter.Result);
        }
    }
}
