using System.Diagnostics;
using System.Linq;
using TextConcordanceLab.Models;
using TextConcordanceLab.Services;

namespace TextConcordanceLab.Tests
{
    public static class ConcordanceTests
    {
        public static void RunAllTests()
        {
            TestWordFrequencyAndUniqueLines();
            TestCaseInsensitivity();
            TestAlphabeticalOrder();
        }

        private static void TestWordFrequencyAndUniqueLines()
        {
            var text = new Text();
            var s1 = new Sentence();
            s1.AddToken(new Word("Peter"));
            s1.AddToken(new Word("Piper"));
            s1.AddToken(new Word("Peter")); // Повтор в той же строке
            s1.AddToken(new Punctuation("."));

            var s2 = new Sentence();
            s2.AddToken(new Word("Piper"));
            s2.AddToken(new Punctuation("!"));

            text.AddSentence(s1);
            text.AddSentence(s2);

            var result = text.BuildConcordance();

            // "peter" встречается 2 раза, но только в 1-й строке
            Debug.Assert(result["peter"].Frequency == 2, "Частота слова 'peter' должна быть 2");
            Debug.Assert(result["peter"].Lines.Count == 1 && result["peter"].Lines.Contains(1), "Строка 1 не должна дублироваться");

            // "piper" встречается 2 раза в строках 1 и 2
            Debug.Assert(result["piper"].Frequency == 2, "Частота слова 'piper' должна быть 2");
            Debug.Assert(result["piper"].Lines.SequenceEqual(new[] { 1, 2 }), "Слово 'piper' должно быть в строках 1 и 2");
        }

        private static void TestCaseInsensitivity()
        {
            var text = new Text();
            var s1 = new Sentence();
            s1.AddToken(new Word("Peck"));
            s1.AddToken(new Word("peck"));
            text.AddSentence(s1);

            var result = text.BuildConcordance();
            Debug.Assert(result.Count == 1, "Слова в разном регистре должны объединяться");
            Debug.Assert(result["peck"].Frequency == 2, "Частота должна быть 2");
        }

        private static void TestAlphabeticalOrder()
        {
            var text = new Text();
            var s1 = new Sentence();
            s1.AddToken(new Word("zebra"));
            s1.AddToken(new Word("apple"));
            text.AddSentence(s1);

            var result = text.BuildConcordance();
            var sorted = new System.Collections.Generic.SortedSet<string>(result.Keys);
            Debug.Assert(sorted.First() == "apple", "Первым словом по алфавиту должно быть 'apple'");
        }
    }
}