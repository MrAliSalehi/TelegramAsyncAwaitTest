using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramDelayTestDemo;

public static class BotLogic
{
    public static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (message.From is null) return;
        await Task.Delay(1, cancellationToken);

        MessageAsync(botClient, message, cancellationToken).GetAwaiter();

        Console.WriteLine($"Message:[{message.Text}]From:[{message.From.Id}]");
    }

    private static async Task MessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        for (var i = 0; i < 20; i++)
            await botClient.SendTextMessageAsync(message.From!.Id, $"{i}-message", cancellationToken: cancellationToken);
    }
}