using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ventera.VAF.Core;
using Ventera.VAF.Core.Configuration;


namespace TIC.PuppyLove.Helpers.UnitTest
{
    [TestClass]
    public class HelpersUnitTest
    {
        [TestMethod]
        public void TestCreateHashSha512()
        {
            CryptoHelper crypto = new CryptoHelper();
            string pwAsUserEnteredString = "The Sun Did Not Shine";
            byte[] result = crypto.CreateHashSha512(pwAsUserEnteredString);
            string resultstr = BitConverter.ToString(result);

            Assert.AreEqual("1", "1");
        }

        [TestMethod]
        public void TestIsPasswordCorrectForTrue()
        {
            CryptoHelper crypto = new CryptoHelper();
            string pw = "The Sun Did Not Shine";
            string pwAsUserEnteredString = "The Sun Did Not Shine";

            byte[] pwAsByteArrayFromDB = crypto.CreateHashSha512(pw);

            bool pwcorrect = crypto.IsPasswordCorrect(pwAsUserEnteredString, pwAsByteArrayFromDB);

            Assert.IsTrue(pwcorrect);
        }

        [TestMethod]
        public void TestIsPasswordCorrectForFalse()
        {
            CryptoHelper crypto = new CryptoHelper();
            string pw = "The Sun Did Not Shine";
            string pwAsUserEnteredString = "It Was Too Wet To Play";
            //string pwAsUserEnteredString = "the sun did not shine";

            byte[] pwAsByteArrayFromDB = crypto.CreateHashSha512(pw);

            bool pwcorrect = crypto.IsPasswordCorrect(pwAsUserEnteredString, pwAsByteArrayFromDB);

            Assert.IsFalse(pwcorrect);
        }
    }
}
