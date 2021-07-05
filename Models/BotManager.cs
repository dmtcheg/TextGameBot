﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using RecurrentTasks;
using System.Threading;

namespace lingames.Models
{
    public class BotManager : IBotManager<LinGamesBot>, IRunnable
    {
        public string WebhookUrl => "";

        public Task GetAndHandleNewUpdatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdateAsync(Update update)
        {
            throw new NotImplementedException();
        }

        public Task RunAsync(ITask currentTask, IServiceProvider scopeServiceProvider, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetWebhookStateAsync(bool enabled)
        {
            throw new NotImplementedException();
        }
    }
}
