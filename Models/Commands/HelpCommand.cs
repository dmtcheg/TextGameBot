using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace lingames.Models
{
    public class HelpCommand : CommandBase<DefaultArgs>
    {
        public HelpCommand() : base("help")
        {
        }
        
        public override async Task<UpdateHandlingResult> HandleCommand(Update update, DefaultArgs args)
        {
            await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id,
                "sorry, i haven't help description yet");
            return UpdateHandlingResult.Handled;
        }
    }
}
