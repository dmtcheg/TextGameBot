﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace lingames.Models
{
    public class StartCommand : CommandBase<DefaultArgs>
    {
        public StartCommand() : base("start")
        {
        }

        public override async Task<UpdateHandlingResult> HandleCommand(Update update, DefaultArgs args)
        {
            await Bot.Client.SendTextMessageAsync(update.Message.Chat.Id,
                "this is word games bot"); // добавить подробное описание игр
            return UpdateHandlingResult.Handled;
        }
    }
}
