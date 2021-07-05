using System;
using System.Collections.Generic;

namespace lingames.Models
{
    public partial class Nouns
    {
        public Nouns(string word)
        {
            Noun = word;
        }
        public string Noun { get; set; }
    }
}
