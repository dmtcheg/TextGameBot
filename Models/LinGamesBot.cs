﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace lingames.Models
{
    public class LinGamesBot : BotBase<LinGamesBot>
    {        
        public LinGamesBot(IOptions<BotOptions<LinGamesBot>> botOptions)
            : base(botOptions)
        {
            
        }

        public override async Task HandleFaultedUpdate(Update update, Exception e)
        {
            //Logger.LogWarning("Unable to handle an update");

            await Client.SendTextMessageAsync(update.Message.Chat.Id,
                    "I don't know what to do with this message",
                    replyToMessageId: update.Message.MessageId);
            
        }

        public override Task HandleUnknownUpdate(Update update)
        {
            //Logger.LogCritical("Exception thrown while handling an update");
            return Task.CompletedTask;
        }
    }
}
