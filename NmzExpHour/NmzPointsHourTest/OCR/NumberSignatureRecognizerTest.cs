using System.Collections.Generic;
using NmzPointsHour.OCR;
using NUnit.Framework;

namespace NmzPointsHourTest.OCR
{
    [TestFixture]
    public class NumberSignatureRecognizerTest
    {
        [Test]
        public void RecognizeANumberFromItsSignature()
        {
            var signature3 = new List<int> {2, 2, 1, 2, 1, 1, 2, 2};
            var numbers = new List<string>{"2", "3", "4"};

            var actual = new NumberSignatureRecognizer().RecognizeSignature(signature3, numbers);

            Assert.AreEqual("3", actual);
        }

        [Test]
        public void UsesTheLastNumberAsDefault()
        {
            var signatureNotFound = new List<int> { 2, 2,8, 2, 1, 1, 2, 2 };
            var numbers = new List<string> { "2", "3", "4" };

            var actual = new NumberSignatureRecognizer().RecognizeSignature(signatureNotFound, numbers);

            Assert.AreEqual("4", actual);
        }
    }
}
