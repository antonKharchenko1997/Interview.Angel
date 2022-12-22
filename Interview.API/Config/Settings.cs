namespace Interview.Angel.Config;

public class Settings
{
    public const string ConfigureSection = "Settings";
    public string TelegramBotToken { get; init; } = default!;
    public string TwitchApiClientId { get; init; } = default!;
    public string TwitchApiAccessToken { get; init; } = default!;
}