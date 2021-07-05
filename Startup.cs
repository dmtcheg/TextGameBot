using lingames.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace lingames
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WordsContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("Words")));
            services.AddScoped(provider => new WordsContext()); // doesn't help

            //var linBotOptions = new BotOptions<LinGamesBot>();
            //Configuration.GetSection("LinGamesBot").Bind(linBotOptions);
            services.AddTelegramBot<LinGamesBot>(Configuration.GetSection("LinGamesBot"))
                .AddUpdateHandler<StartCommand>()
                .AddUpdateHandler<HelpCommand>()
                .AddUpdateHandler<RandomWordCommand>()
                .AddUpdateHandler<HangmanCommand>()
                .AddUpdateHandler<KeywordCommand>()
                .Configure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {//need proxy and ngrok
                app.UseDeveloperExceptionPage();

                var source = new CancellationTokenSource();
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("## Press Enter to stop bot manager...");
                    Console.ReadLine();
                    source.Cancel();
                });

                Task.Factory.StartNew(async () =>
                {
                    var botManager = app.ApplicationServices.GetRequiredService<IBotManager<LinGamesBot>>();

                    // make sure webhook is disabled so we can use long-polling
                    await botManager.SetWebhookStateAsync(false);

                    while (!source.IsCancellationRequested)
                    {
                        await Task.Delay(6000);
                        await botManager.GetAndHandleNewUpdatesAsync();
                    }
                })
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        throw t.Exception;
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                    appBuilder.Run(context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        return Task.CompletedTask;
                    })
                );

                app.UseTelegramBotWebhook<LinGamesBot>();
            }

            app.Run(async context => { await context.Response.WriteAsync("word games bot"); });
        }
    }
}