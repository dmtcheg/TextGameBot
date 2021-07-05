using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace lingames.Models
{
    public class KeywordCommand : CommandBase<DefaultArgs>
    {
        private WordsContext _context;
        public KeywordCommand() : base("keyword")
        {
        }
        public KeywordCommand(DbContext context) : base("keyword")
        {
            _context = context as WordsContext;
        }
        
        public override async Task<UpdateHandlingResult> HandleCommand(Update update, DefaultArgs args)
        {
            string word;
            using (_context)
            {
                word = SharedMethods.GetRandomWord(_context);
            }
            await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id, word);
            var message = update.Message.Text;
            var points = 0;
            var used = new HashSet<string>();
            var watch = new Stopwatch();
            watch.Start();

            while (watch.Elapsed < TimeSpan.FromSeconds(100))
            {
                if (message.All(word.Contains) &&
                    _context.Nouns.Contains(new Nouns(message)) &&
                    used.Add(message))
                {
                    await Bot.Client.SendTextMessageAsync
                        (update.Message.Chat.Id, $"правильно! слов найдено: {++points}");
                }
                else
                {
                    await Bot.Client.SendTextMessageAsync
                        (update.Message.Chat.Id, "не засчитано");
                }
            }
            await Bot.Client.SendTextMessageAsync
                        (update.Message.Chat.Id, $"итог: {points}");
            points = 0;
            return UpdateHandlingResult.Handled;
        }
    }
}
