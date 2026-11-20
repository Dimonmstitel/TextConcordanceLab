using System;
using System.Collections.Generic;
using TextConcordanceLab.Models;

namespace TextConcordanceLab.Services
{
    public static class ConcordanceAnalyzer
    {
        public static Dictionary<string, (int Frequency, SortedSet<int> Lines)> BuildConcordance(this Text textObj)
        {
            var concordance = new Dictionary<string, (int Frequency, SortedSet<int> Lines)>();
            int lineNumber = 1;

            // Проходим напрямую по предложениям (строкам) из модели Text
            foreach (var sentence in textObj.Sentences)
            {
                // Перебираем токены внутри предложения
                foreach (var token in sentence.Tokens)
                {
                    // Берем только объекты класса Word, игнорируя Punctuation (знаки препинания)
                    if (token is Word wordToken)
                    {
                        string word = wordToken.Value.ToLower();

                        if (string.IsNullOrWhiteSpace(word))
                            continue;

                        if (!concordance.ContainsKey(word))
                        {
                            concordance[word] = (Frequency: 0, Lines: new SortedSet<int>());
                        }

                        var entry = concordance[word];
                        entry.Frequency++;
                        entry.Lines.Add(lineNumber);
                        concordance[word] = entry;
                    }
                }
                lineNumber++;
            }

            return concordance;
        }

        public static void PrintConcordance(Dictionary<string, (int Frequency, SortedSet<int> Lines)> concordance)
        {
            var sortedWords = new SortedSet<string>(concordance.Keys);

            Console.WriteLine("Конкорданс текста:");
            Console.WriteLine(new string('-', 40));

            foreach (string word in sortedWords)
            {
                var data = concordance[word];
                string lineNumbersStr = string.Join(" ", data.Lines);
                Console.WriteLine($"{word,-18} {data.Frequency}: {lineNumbersStr}");
            }
        }
    }
}