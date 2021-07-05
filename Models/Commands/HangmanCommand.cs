using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace lingames.Models
{
    public class HangmanCommand : CommandBase<DefaultArgs>
    {
        private WordsContext _context;
        public HangmanCommand() : base("hangman")
        {
        }
        public HangmanCommand(DbContext context) : base("hangman")
        {
            _context = context as WordsContext;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, DefaultArgs args)
        {
            string word;
            word = SharedMethods.GetRandomWord(_context);
            var mask = new StringBuilder(word);
            var tryCount = 0;
            for (int i = 1; i < word.Length - 1; i++)
            {
                mask[i] = '*';
            }
            while (mask.ToString().Contains('*'))
            {
                await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id, mask.ToString());
                var c = update.Message.Text[0];
                tryCount++;
                for (int i = word.IndexOf(c); i > -1; i = word.IndexOf(c, i + 1))
                    mask[i] = c;
            }
            await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id, 
                $"использовано попыток: {tryCount}");
            return UpdateHandlingResult.Handled;
        }
    }
}