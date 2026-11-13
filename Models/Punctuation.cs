namespace TextConcordanceLab.Models
{
    public class Punctuation
    {
        public string Symbol { get; set; }

        public Punctuation(string symbol)
        {
            Symbol = symbol;
        }

        public override string ToString() => Symbol;
    }
}