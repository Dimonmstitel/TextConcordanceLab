using System.Collections.Generic;
using System.Text;

namespace TextConcordanceLab.Models
{
    public class Sentence
    {
        public List<object> Tokens { get; set; } = new List<object>();

        public void AddToken(object token) => Tokens.Add(token);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (i > 0 && Tokens[i] is Word)
                {
                    sb.Append(" ");
                }
                sb.Append(Tokens[i].ToString());
            }
            return sb.ToString();
        }
    }
}