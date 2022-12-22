using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using TwitchLib.Api;
using TwitchLib.Api.Core;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace Interview.Angel.Core;


public class Angel
{
    private readonly ITelegramBotClient _telegramBotClient;

    public Angel(string telegramBotToken, string twitchApiToken, string twitchApiClientId)
    {
        _telegramBotClient = new TelegramBotClient(telegramBotToken);
        _telegramBotClient.StartReceiving<TelegramBotMessagesHandler>(new ReceiverOptions
        {
            AllowedUpdates = new []
            {
                UpdateType.Message
            }
        });
        
        var liveStreamMonitorService = new LiveStreamMonitorService(new TwitchAPI(settings: new ApiSettings
        {
            AccessToken = twitchApiToken,
            ClientId = twitchApiClientId
        }));

        liveStreamMonitorService.OnStreamOnline += OnStreamOnline;
        
        liveStreamMonitorService.SetChannelsByName(new List<string>
        {
         "fandercs"   
        });
        liveStreamMonitorService.Start();
    }

    private async void OnStreamOnline(object? sender, OnStreamOnlineArgs onStreamOnlineArgs)
    {
        var game = onStreamOnlineArgs.Stream.GameName ?? string.Empty;
        await _telegramBotClient.SendTextMessageAsync(123123, $"Stream about {game}").ConfigureAwait(false);
    }
}