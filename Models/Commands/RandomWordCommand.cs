using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace lingames.Models
{
    public class RandomWordCommand : CommandBase<DefaultArgs>
    {
        private WordsContext _context;
        public RandomWordCommand(DbContext context) : base("random_word")
        {
            _context = context as WordsContext;
        }
        public RandomWordCommand() : base("random_word")
        {
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, DefaultArgs args)
        {
            string word;
            var rnd = new Random();
            var skipCount = rnd.Next(0, 51300); // fix 51300 to dbset.count()-1

            using (var wordsdb = new WordsContext())
            {
                word = wordsdb.Nouns.Skip(skipCount).Take(1).FirstOrDefault().Noun;
            }

            await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id, word);
            return UpdateHandlingResult.Handled;
        }
    }
}