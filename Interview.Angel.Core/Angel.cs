using Telegram.Bot;
using TwitchLib.Api;
using TwitchLib.Api.Core;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace Interview.Angel.Core;

public class Angel
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly LiveStreamMonitorService _liveStreamMonitorService;

    public Angel(string telegramBotToken, string twitchApiToken, string twitchApiClientId)
    {
        _telegramBotClient = new TelegramBotClient(telegramBotToken);
        _liveStreamMonitorService = new LiveStreamMonitorService(new TwitchAPI(settings: new ApiSettings
        {
            AccessToken = twitchApiToken,
            ClientId = twitchApiClientId
        }));

        _liveStreamMonitorService.OnStreamOnline += OnStreamOnline;
        _liveStreamMonitorService.Start();
    }

    private void OnStreamOnline(object? sender, OnStreamOnlineArgs onStreamOnlineArgs)
    {
        var game = onStreamOnlineArgs.Stream.GameName;
        _telegramBotClient.SendTextMessageAsync(123123, $"Stream about {game}");
    }
}