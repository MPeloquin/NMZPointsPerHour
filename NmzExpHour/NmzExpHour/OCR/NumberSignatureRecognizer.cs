using System;
using System.Collections.Generic;
using System.Linq;

namespace NmzExpHour.OCR
{
    public class NumberSignatureRecognizer
    {
        private readonly List<Tuple<string, List<int>>> signatures;

        public NumberSignatureRecognizer()
        {
            signatures = new List<Tuple<string, List<int>>>
            {		
                new Tuple<string, List<int>>("0", new List<int> {3, 2, 2, 2, 2, 2, 2, 3}),
                new Tuple<string, List<int>>("1", new List<int> {1, 2, 1, 1, 1, 1, 1, 3}),
                new Tuple<string, List<int>>("3", new List<int> {2, 2, 1, 2, 1, 1, 2, 2}),
                new Tuple<string, List<int>>("5", new List<int> {4, 1, 1, 3, 1, 1, 2, 2}),
                new Tuple<string, List<int>>("9", new List<int> {3, 2, 2, 2, 3, 1, 1, 1}),
            };
        }

        private bool IsSignature(List<int> rows, List<int> signature)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i] != signature[i])
                    return false;
            }
            return true;
        }

        public string RecognizeSignature(List<int> signature, List<string> numbers)
        {
            foreach (var number in numbers)
            {
                if (signatures.Any(sig => sig.Item1 == number && IsSignature(signature, sig.Item2)))
                {
                    return number;
                }
            }

            return numbers.Last();
        }
    }
}