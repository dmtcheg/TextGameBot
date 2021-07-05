using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace lingames.Models
{
    public class SharedMethods
    {        
        /// <summary> возвращает случайную строку из таблицы</summary>
        public static string GetRandomWord(WordsContext context)
        {
            var rnd = new Random();
            var skipCount = rnd.Next(0, 51300); // fix 51300 to dbset.count()-1
            return context.Nouns.Skip(skipCount).Take(1).FirstOrDefault().Noun;
        }
    }
}
