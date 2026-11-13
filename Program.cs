using System;
using TextConcordanceLab.Models;
using TextConcordanceLab.Services;
using TextConcordanceLab.Tests;

namespace TextConcordanceLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Запуск тестов
            ConcordanceTests.RunAllTests();
            Console.WriteLine("Все тесты успешно пройдены!\n");

            // 2. Формирование текста из примера
            var text = new Text();

            var line1 = new Sentence();
            foreach (var w in new[] { "Peter", "Piper", "picked", "a", "peck", "of", "pickled", "peppers" })
                line1.AddToken(new Word(w));
            line1.AddToken(new Punctuation("."));
            foreach (var w in new[] { "A", "peck", "of", "pickled" })
                line1.AddToken(new Word(w));

            var line2 = new Sentence();
            foreach (var w in new[] { "peppers", "Peter", "Piper", "picked" })
                line2.AddToken(new Word(w));
            line2.AddToken(new Punctuation("."));
            foreach (var w in new[] { "If", "Peter", "Piper", "picked", "a", "peck", "of" })
                line2.AddToken(new Word(w));

            var line3 = new Sentence();
            foreach (var w in new[] { "pickled", "peppers", "where", "is", "the", "peck", "that", "Peter", "Piper", "picked" })
                line3.AddToken(new Word(w));
            line3.AddToken(new Punctuation("?"));

            text.AddSentence(line1);
            text.AddSentence(line2);
            text.AddSentence(line3);

            // 3. Построение и вывод конкорданса
            var concordance = text.BuildConcordance();
            ConcordanceAnalyzer.PrintConcordance(concordance);
        }
    }
}