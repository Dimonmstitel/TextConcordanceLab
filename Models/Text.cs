using System.Collections.Generic;

namespace TextConcordanceLab.Models
{
    public class Text
    {
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();

        public void AddSentence(Sentence sentence) => Sentences.Add(sentence);

        // Возвращает строки текста (в рамках задачи одна строка = одно предложение или строка исходного файла)
        public IEnumerable<string> GetLines()
        {
            foreach (var sentence in Sentences)
            {
                yield return sentence.ToString();
            }
        }
    }
}